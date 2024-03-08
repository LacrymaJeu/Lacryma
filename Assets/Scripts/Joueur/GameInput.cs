using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class gameInput : MonoBehaviour {
    //Force de saut
    [SerializeField] private float jumpForce = 5f;

    // Distance de v�rification pour d�terminer si le joueur est au sol
    [SerializeField] private float groundCheckDistance = 0.1f;

    // Gestionnaire d'actions du joueur
    private PlayerInputActions playerInputActions;

    // player rigidbody
    private Rigidbody playerRigidBody;

    // le joueur touche au sol
    private bool isGrounded = true;

    // M�thode appel�e au d�marrage de l'objet
    private void Awake() {
        // Initialisation du gestionnaire d'actions du joueur
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        // R�cup�ration du composant Rigidbody attach� � cet objet
        playerRigidBody = GetComponent<Rigidbody>();
    }

    // M�thode appel�e � chaque frame physique
    private void FixedUpdate() {
        // Effectuer une v�rification au sol
        isGrounded = GroundCheck();
    }

    // V�rifie si le joueur est au sol
    private bool GroundCheck() {
        // Lance un rayon vers le bas pour v�rifier si le joueur est au sol si le rayon touche le sol cela veut dire que le joueur touche le sol
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance)) {
            return true; // Le joueur est au sol si le rayon touche un collider dans groundCheckDistance
        }
        //le rayon ne touche rien le joueur n'est pas au sol
        return false;
    }

    // Renvoie le vecteur de d�placement normalis� du joueur
    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }

    // M�thode pour g�rer le saut du joueur
    public void Jump(InputAction.CallbackContext context) {
        // V�rifie si l'entr�e de saut est effectu�e et si le joueur est au sol
        if (context.performed && isGrounded) {
            //Debug.Log("jump");
            // force pour faire sauter
            playerRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}