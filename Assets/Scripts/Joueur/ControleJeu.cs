using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Contrôle la logique du Player Input System

public class ControleJeu : MonoBehaviour {
    // Force de saut
    [SerializeField] private float forceSaut = 5f;

    // Distance de vérification pour déterminer si le joueur est au sol
    [SerializeField] private float groundCheckDistance = 0.1f;
    //grosseur du box cast
    float boxCastSize = 0.5f;

    [SerializeField] private float maxJumpVelocity = .2f;

    public Player scriptJoueur;

    // Gestionnaire d'actions du joueur
    private PlayerInputActions playerInputActions;

    // player rigidbody
    private Rigidbody joueurRigidBody;

    // Le joueur touche au sol
    private bool toucheSol = true;

    //savoir si le joueur touche le sol
    private bool touchaitSol = true;

    // Indicateur de sprint
    private bool joueurSprint = false;

    private float mouvementOrigine;

    // Durée du cooldown de saut (en secondes)
    private float jumpCooldownDuration = 0.3f;

    // Timer pour le cooldown de saut
    private float jumpCooldownTimer = 0f;





    // Méthode appelée au démarrage de l'objet
    private void Awake() {
        // Initialisation du gestionnaire d'actions du joueur
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        // Récupération du composant Rigidbody attaché à cet objet
        joueurRigidBody = GetComponent<Rigidbody>();

        // Associer la méthode SprintStarted à l'action de sprint (au début de la pression)
        playerInputActions.Player.Sprint.started += DebutSprint;
        // Associer la méthode SprintCanceled à l'action de sprint (à la fin de la pression)
        playerInputActions.Player.Sprint.canceled += SprintCanceled;

        //movement original
        mouvementOrigine = scriptJoueur.vitesseDep;
    }


    private void Update() {
        // regard si le joueur touche le sol apres le sprint
        if (scriptJoueur != null && !joueurSprint && !touchaitSol && toucheSol) {
            // revien au movement original quand le joueur touche le sol
            scriptJoueur.vitesseDep = mouvementOrigine;
        }

        // Update the flag indicating whether the player was grounded in the previous frame
        touchaitSol = toucheSol;

        // Met à jour le timer de cooldown de saut
        if (jumpCooldownTimer > 0f) {
            jumpCooldownTimer -= Time.deltaTime;
        }
    }

    // Méthode appelée à chaque frame physique
    private void FixedUpdate() {
        // Effectuer une vérification au sol
        toucheSol = GroundCheck();
    }

    // Vérifie si le joueur est au sol
    private bool GroundCheck() {
        // Crée un boxcast en utilisant la position du joueur et la direction vers le bas
        // pour détecter les collisions avec les sols potentiels
        RaycastHit hit;
        if (Physics.BoxCast(transform.position, Vector3.one * boxCastSize, Vector3.down, out hit, Quaternion.identity, groundCheckDistance)) {
            return true; // Le joueur est au sol si le boxcast touche un collider dans groundCheckDistance
        }
        // Aucune collision avec le sol détectée
        return false;
    }

    // Méthode pour gérer le saut du joueur
    public void Jump(InputAction.CallbackContext context) {
        // Vérifie si l'entrée de saut est effectuée et si le joueur est au sol
        if (context.performed && toucheSol) {
            // Vérifie si la vélocité verticale actuelle est inférieure à la vélocité maximale
            if (Mathf.Abs(joueurRigidBody.velocity.y) < maxJumpVelocity) {
                // Applique une force de saut uniquement si la vélocité verticale est en dessous de la limite
                joueurRigidBody.AddForce(Vector3.up * forceSaut, ForceMode.Impulse);
            }
        }

        // Vérifie si le cooldown de saut est terminé
        if (jumpCooldownTimer <= 0f) {
            // Vérifie si l'entrée de saut est effectuée et si le joueur est au sol
            if (context.performed && toucheSol) {
                // Vérifie si la vélocité verticale actuelle est inférieure à la vélocité maximale
                if (Mathf.Abs(joueurRigidBody.velocity.y) < maxJumpVelocity) {
                    // Applique une force de saut uniquement si la vélocité verticale est en dessous de la limite
                    joueurRigidBody.AddForce(Vector3.up * forceSaut, ForceMode.Impulse);

                    // Active le cooldown de saut
                    jumpCooldownTimer = jumpCooldownDuration;
                }
            }
        }
    }

    // Méthode pour gérer le sprint du joueur
    // Méthode appelée lorsque le sprint commence
public void DebutSprint(InputAction.CallbackContext context) {
    joueurSprint = true; // Marquer que le joueur sprinte
    // Augmenter moveSpeed uniquement si le joueur est au sol ou s'il sprinte déjà dans les airs
    if (toucheSol || joueurSprint) {
        if (scriptJoueur != null) {
            scriptJoueur.vitesseDep += 2; // moveSpeed + 2
        }
    }
}

    // Méthode appelée lorsque le sprint se termine
    public void SprintCanceled(InputAction.CallbackContext context) {
        joueurSprint = false; // Marquer que le joueur a arrêté de sprinter
       // Debug.Log("Sprint Fin");

        if (scriptJoueur != null && toucheSol) { 
                scriptJoueur.vitesseDep = mouvementOrigine; //retourne a sa valeur initial
        }
    }

    // Méthode pour obtenir le vecteur de mouvement normalisé du joueur
    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }
}