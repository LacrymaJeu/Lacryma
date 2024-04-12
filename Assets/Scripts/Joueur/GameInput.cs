using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {
    // Force de saut
    [SerializeField] private float jumpForce = 5f;

    // Distance de v�rification pour d�terminer si le joueur est au sol
    [SerializeField] private float groundCheckDistance = 0.1f;

    // R�f�rence au script Player
    public Player playerScript;

    // R�f�rence au Rigidbody du joueur
    private Rigidbody playerRigidBody;

    // Le joueur touche-t-il le sol ?
    public bool isGrounded = true;



    // M�thode appel�e � chaque frame physique
    private void FixedUpdate() {
        // Effectuer une v�rification au sol
        isGrounded = GroundCheck();
    }

    // V�rifie si le joueur est au sol
    public bool GroundCheck() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance)) {
            // Dessine un rayon de d�bogage pour visualiser la v�rification au sol
            Debug.DrawRay(transform.position, Vector3.down * groundCheckDistance, Color.green);
            return true;
        }
        // Dessine un rayon de d�bogage pour visualiser la v�rification au sol
        Debug.DrawRay(transform.position, Vector3.down * groundCheckDistance, Color.red);
        return false;
    }

    // M�thode pour g�rer le saut du joueur
    public void Jump(InputAction.CallbackContext context) {
        // V�rifie si l'entr�e de saut est effectu�e et si le joueur est au sol
        if (context.performed && isGrounded) {
            // Applique une force vers le haut pour faire sauter le joueur
            playerRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    // M�thode pour v�rifier si le joueur est en train de sauter
    public bool IsJumping() {
        // Retourne vrai si le joueur est en train de sauter, faux sinon
        return !isGrounded;
    }
}
