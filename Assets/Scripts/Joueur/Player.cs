using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Contr�le la logique du joueur

public class Player : MonoBehaviour {

    // R�f�rence � la cam�ra
    public Transform cam;

    // Vitesse de d�placement du joueur
    public float vitesseDep = 7f; 

    // R�f�rence � l'input du jeu
    [SerializeField] private ControleJeu gameInput; // La variable doit �tre en anglais

    // Indique si le joueur est en train de marcher
    private bool ilMarche;

    // Indique si le joueur est en train de courir
    private bool ilCours;

    public static bool peutBouger = true;

    private void Update() {
            // R�cup�re le vecteur de d�placement normalis� du gameInput
            Vector2 inputVector = gameInput.GetMovementVectorNormalized(); // Variable doit �tre en anglais

            // Convertit le vecteur d'input en espace monde relatif � la cam�ra
            Vector3 camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 moveDir = camForward * inputVector.y + cam.right * inputVector.x;

            // Applique le d�placement

            float moveDistance = vitesseDep * Time.deltaTime;
            float joueurRadius = .3f;
            float playerHeight = 2f;
            bool peutBouger = !Physics.CapsuleCast(transform.position, transform.position +Vector3.up * playerHeight, joueurRadius, moveDir,moveDistance );
            if (peutBouger) {
                transform.position += moveDir * vitesseDep * Time.deltaTime;
            }

            // Oriente le joueur vers la direction de la cam�ra
            if (moveDir.magnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDir, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }

            // Gestion de l'animation marche
            ilMarche = moveDir != Vector3.zero;

            ilCours = vitesseDep > 3f;
    } 
                 
        

     //verification si le joueur marche ou pas
     public bool IlMarche() {
        return ilMarche;
    }

    // V�rifie si le joueur court
    public bool IlCours() {
        return ilCours;
    }
}
