using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //Variable pour le viseur
    public GameObject viseur;

    // R�f�rence au composant Rigidbody
    private Rigidbody joueurRigidbody;

    // R�f�rence � la cam�ra
    public Transform cam;

    // Vitesse de d�placement du joueur
    public float vitesseDep = 5f;

    // R�f�rence � l'input du jeu
    [SerializeField] private ControleJeu gameInput;

    // Indique si le joueur est en train de marcher
    private bool ilMarche;

    // Indique si le joueur est en train de courir
    private bool ilCours;

    // Bool�en indiquant si le joueur est en mesure de bouger pendant un dialogue
    public static bool peutBougerDialogue = true;

    // Hauteur totale du joueur
    [SerializeField] private float playerHeight = 2f;

    // Rayon du joueur utilis� dans le CapsuleCast
    [SerializeField] private float joueurRadius = 0.5f;

    // Constantes pour les hauteurs de saut et la marge pour le saut
    private const float hauteurSaut = 2f;
    private const float margeSaut = 0.1f;

    private void Awake() {
        joueurRigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked; // Verrouille le curseur.
        Cursor.visible = false; // Cache le curseur.
        viseur.SetActive(true); //activer le viseur 
    }

    private void Update() {
        // V�rifie si le joueur est en dialogue
        if (peutBougerDialogue) {
            // R�cup�re le vecteur de d�placement normalis� du gameInput
            Vector2 inputVector = gameInput.GetMovementVectorNormalized();

            // Convertit le vecteur d'input en espace monde relatif � la cam�ra
            Vector3 camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 moveDir = camForward * inputVector.y + cam.right * inputVector.x;

            // Appliquer le d�placement
            float moveDistance = vitesseDep * Time.deltaTime;

            // Calculez la position de d�but du CapsuleCast au niveau du joueur
            Vector3 positionDebut = transform.position - Vector3.up * (playerHeight / 2f);

            // Calculez la position de fin du CapsuleCast au niveau de la t�te du joueur
            Vector3 positionFin = transform.position + Vector3.up * (playerHeight / 2f);

            // Utilisez positionDebut et positionFin dans votre CapsuleCast
            bool canMove = !Physics.CapsuleCast(positionDebut, positionFin, joueurRadius, moveDir, moveDistance);

            // Permet de bouger diagonalement quand le personnage est bloqu� par un mur
            if (!canMove) {
                Vector3 moveDirX = new Vector3(moveDir.x, 0, 0);
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z);

                // Le joueur essaiera d'abord de se d�placer horizontalement, puis verticalement
                moveDir = !Physics.CapsuleCast(positionDebut, positionFin, joueurRadius, moveDirX, moveDistance) ? moveDirX : moveDirZ;
            }

            // D�placer le joueur si le mouvement est autoris�
            transform.position += moveDir * vitesseDep * Time.deltaTime;

            // Orienter le joueur vers la direction de la cam�ra
            if (moveDir.magnitude > 0.1f) {
                Quaternion targetRotation = Quaternion.LookRotation(moveDir, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }

            // Gestion de l'animation de marche
            ilMarche = moveDir != Vector3.zero;
            ilCours = moveDir != Vector3.zero && vitesseDep > 5f; // Mise � jour de la condition de course
        }
    }

    // V�rifie si le joueur marche
    public bool IlMarche() {
        return ilMarche;
    }

    // V�rifie si le joueur court
    public bool IlCours() {
        return ilCours;
    }
}
