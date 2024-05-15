using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Contr�le la logique du joueur

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

            // Applique le d�placement
            float moveDistance = vitesseDep * Time.deltaTime;
            float joueurRadius = 0.3f;
            float playerHeight = 2f;

            // Obtenez la hauteur totale du joueur
            float hauteurJoueur = 2f; // D�finissez ceci en fonction de la taille r�elle de votre joueur

            // D�finissez une variable pour repr�senter la moiti� de la hauteur du joueur
            float demiHauteurJoueur = hauteurJoueur / 2f;

            // Calculez la position de d�but du CapsuleCast en utilisant la variable demiHauteurJoueur
            Vector3 positionDebut = transform.position - Vector3.up * demiHauteurJoueur; // Position de d�part au bas du joueur

            // Calculez la position de fin du CapsuleCast en utilisant la variable demiHauteurJoueur
            Vector3 positionFin = transform.position + Vector3.up * demiHauteurJoueur; // Position de fin au sommet du joueur

            // Utilisez positionDebut et positionFin dans votre CapsuleCast
            bool canMove = !Physics.CapsuleCast(positionDebut, positionFin, joueurRadius, moveDir, moveDistance);

            // Permet de bouger diagonalement quand le personnage est bloqu� par un mur
            if (!canMove) {
                Vector3 moveDirX = new Vector3(moveDir.x, 0, 0);
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, joueurRadius, moveDirX, moveDistance);
                if (canMove) {
                    moveDir = moveDirX;
                } else {
                    Vector3 moveDirZ = new Vector3(0, 0, moveDir.z);
                    canMove = !Physics.CapsuleCast(transform.position, transform.position - Vector3.up * playerHeight, joueurRadius, moveDirZ, moveDistance);

                    if (canMove) {
                        moveDir = moveDirZ;
                    } else {
                        // V�rification si le joueur peut sauter m�me s'il est bloqu� par un mur
                        bool canJump = Physics.Raycast(transform.position, Vector3.down, playerHeight / hauteurSaut + margeSaut);
                        if (canJump) {
                            moveDir = Vector3.zero;
                        }
                    }
                }
            }

            // D�place le joueur si le mouvement est autoris�
            if (canMove) {
                transform.position += moveDir * vitesseDep * Time.deltaTime;
            }

            // Oriente le joueur vers la direction de la cam�ra
            if (moveDir.magnitude > 0.1f) {
                Quaternion targetRotation = Quaternion.LookRotation(moveDir, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }

            // Gestion de l'animation marche
            ilMarche = moveDir != Vector3.zero;
            ilCours = vitesseDep > 5f;
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
