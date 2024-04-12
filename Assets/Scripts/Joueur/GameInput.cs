using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {
    // Force de saut
    [SerializeField] private float jumpForce = 5f;

    // Distance de vérification pour déterminer si le joueur est au sol
    [SerializeField] private float groundCheckDistance = 0.1f;

    // Référence au script Player
    public Player playerScript;

    // Référence au Rigidbody du joueur
    private Rigidbody playerRigidBody;

    // Le joueur touche-t-il le sol ?
    public bool isGrounded = true;



    // Méthode appelée à chaque frame physique
    private void FixedUpdate() {
        // Effectuer une vérification au sol
        isGrounded = GroundCheck();
    }

    // Vérifie si le joueur est au sol
    public bool GroundCheck() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance)) {
            // Dessine un rayon de débogage pour visualiser la vérification au sol
            Debug.DrawRay(transform.position, Vector3.down * groundCheckDistance, Color.green);
            return true;
        }
        // Dessine un rayon de débogage pour visualiser la vérification au sol
        Debug.DrawRay(transform.position, Vector3.down * groundCheckDistance, Color.red);
        return false;
    }

    // Méthode pour gérer le saut du joueur
    public void Jump(InputAction.CallbackContext context) {
        // Vérifie si l'entrée de saut est effectuée et si le joueur est au sol
        if (context.performed && isGrounded) {
            // Applique une force vers le haut pour faire sauter le joueur
            playerRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    // Méthode pour vérifier si le joueur est en train de sauter
    public bool IsJumping() {
        // Retourne vrai si le joueur est en train de sauter, faux sinon
        return !isGrounded;
    }
}
