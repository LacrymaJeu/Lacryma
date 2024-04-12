using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Contrôle la logique du joueur

public class Player : MonoBehaviour {

    // Référence au composant Rigidbody
    private Rigidbody joueurRigidbody; 

    // Référence à la caméra
    public Transform cam;

    // Vitesse de déplacement du joueur
    public float vitesseDep = 7f; 

    // Référence à l'input du jeu
    [SerializeField] private ControleJeu gameInput; 

    // Indique si le joueur est en train de marcher
    private bool ilMarche;

    // Indique si le joueur est en train de courir
    private bool ilCours;

 

    public static bool peutBougerDialogue = true;


    private void Awake() {
        joueurRigidbody = GetComponent<Rigidbody>();
    }
    private void Update() {
        // Vérifie si le joueur est en dialogue
        if (peutBougerDialogue)
        {
            // Récupère le vecteur de déplacement normalisé du gameInput
            Vector2 inputVector = gameInput.GetMovementVectorNormalized(); // Variable doit être en anglais

            // Convertit le vecteur d'input en espace monde relatif à la caméra
            Vector3 camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 moveDir = camForward * inputVector.y + cam.right * inputVector.x;

            // Applique le déplacement

            float moveDistance = vitesseDep * Time.deltaTime;
            float joueurRadius = .3f;
            float playerHeight = 2f;
            bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, joueurRadius, moveDir, moveDistance);
            if (canMove)
            {
                transform.position += moveDir * vitesseDep * Time.deltaTime;
            }

            // Oriente le joueur vers la direction de la caméra
            if (moveDir.magnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDir, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }

            // Gestion de l'animation marche
            ilMarche = moveDir != Vector3.zero;

            ilCours = vitesseDep > 5f;
        }
           

           
    } 
                 
        

     //verification si le joueur marche ou pas
     public bool IlMarche() {
        return ilMarche;
    }

    // Vérifie si le joueur court
    public bool IlCours() {
        return ilCours;
    }

}
