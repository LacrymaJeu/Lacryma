using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {

    // Référence à la caméra
    public Transform cam;

    // Vitesse de déplacement du joueur
    [SerializeField] private float moveSpeed = 7f;

    // Référence à l'input du jeu
    [SerializeField] private gameInput gameInput;

    // Indique si le joueur est en train de marcher
    private bool isWalking;

    public static bool canMove = true;

    private void Update() {
        if (canMove)
        {
            // Récupère le vecteur de déplacement normalisé du gameInput
            Vector2 inputVector = gameInput.GetMovementVectorNormalized();

            // Convertit le vecteur d'input en espace monde relatif à la caméra
            Vector3 camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 moveDir = camForward * inputVector.y + cam.right * inputVector.x;

            // Applique le déplacement
            transform.position += moveDir * moveSpeed * Time.deltaTime;

            // Oriente le joueur vers la direction de la caméra
            if (moveDir.magnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDir, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }

            // Gestion de l'animation
            isWalking = moveDir != Vector3.zero;
        } else
        {
            isWalking = false;
        }
    }

    // verification si le joueur marche ou pas
    public bool IsWalking() {
        return isWalking;
    }

}
