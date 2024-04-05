using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Contr�le la logique du joueur

public class player : MonoBehaviour {

    // R�f�rence � la cam�ra
    public Transform cam;

    // Vitesse de d�placement du joueur
    public float moveSpeed = 7f; // Si tu change le nom de cette variable en anglais sa cause des problemes de sprint. Trouve pourquoi!

    // R�f�rence � l'input du jeu
    [SerializeField] private ControleJeu gameInput; // La variable doit �tre en anglais

    // Indique si le joueur est en train de marcher
    private bool marche;

    public static bool peutBouger = true;

    private void Update() {
        

        if (peutBouger)
        {
            // R�cup�re le vecteur de d�placement normalis� du gameInput
            Vector2 inputVector = gameInput.GetMovementVectorNormalized(); // Variable doit �tre en anglais

            // Convertit le vecteur d'input en espace monde relatif � la cam�ra
            Vector3 camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 moveDir = camForward * inputVector.y + cam.right * inputVector.x;

            // Applique le d�placement

            float moveDistance = moveSpeed * Time.deltaTime;
            float joueurRadius = .3f;
            float playerHeight = 2f;
            bool canMove = !Physics.CapsuleCast(transform.position, transform.position +Vector3.up * playerHeight, joueurRadius, moveDir,moveDistance );
            if (canMove) {
                transform.position += moveDir * moveSpeed * Time.deltaTime;
            }

            // Oriente le joueur vers la direction de la cam�ra
            if (moveDir.magnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDir, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }

            // Gestion de l'animation
            marche = moveDir != Vector3.zero;
        } else
        {
            marche = false;
        }
                 
        }

     //verification si le joueur marche ou pas
    public bool IsWalking() {
        return marche;
     }
  }
