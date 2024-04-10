using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Contr�le la logique du Player Input System

public class ControleJeu : MonoBehaviour {
    // Force de saut
    [SerializeField] private float forceSaut = 5f;

    // Distance de v�rification pour d�terminer si le joueur est au sol
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

    // Dur�e du cooldown de saut (en secondes)
    private float jumpCooldownDuration = 0.3f;

    // Timer pour le cooldown de saut
    private float jumpCooldownTimer = 0f;





    // M�thode appel�e au d�marrage de l'objet
    private void Awake() {
        // Initialisation du gestionnaire d'actions du joueur
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        // R�cup�ration du composant Rigidbody attach� � cet objet
        joueurRigidBody = GetComponent<Rigidbody>();

        // Associer la m�thode SprintStarted � l'action de sprint (au d�but de la pression)
        playerInputActions.Player.Sprint.started += DebutSprint;
        // Associer la m�thode SprintCanceled � l'action de sprint (� la fin de la pression)
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

        // Met � jour le timer de cooldown de saut
        if (jumpCooldownTimer > 0f) {
            jumpCooldownTimer -= Time.deltaTime;
        }
    }

    // M�thode appel�e � chaque frame physique
    private void FixedUpdate() {
        // Effectuer une v�rification au sol
        toucheSol = GroundCheck();
    }

    // V�rifie si le joueur est au sol
    private bool GroundCheck() {
        // Cr�e un boxcast en utilisant la position du joueur et la direction vers le bas
        // pour d�tecter les collisions avec les sols potentiels
        RaycastHit hit;
        if (Physics.BoxCast(transform.position, Vector3.one * boxCastSize, Vector3.down, out hit, Quaternion.identity, groundCheckDistance)) {
            return true; // Le joueur est au sol si le boxcast touche un collider dans groundCheckDistance
        }
        // Aucune collision avec le sol d�tect�e
        return false;
    }

    // M�thode pour g�rer le saut du joueur
    public void Jump(InputAction.CallbackContext context) {
        // V�rifie si l'entr�e de saut est effectu�e et si le joueur est au sol
        if (context.performed && toucheSol) {
            // V�rifie si la v�locit� verticale actuelle est inf�rieure � la v�locit� maximale
            if (Mathf.Abs(joueurRigidBody.velocity.y) < maxJumpVelocity) {
                // Applique une force de saut uniquement si la v�locit� verticale est en dessous de la limite
                joueurRigidBody.AddForce(Vector3.up * forceSaut, ForceMode.Impulse);
            }
        }

        // V�rifie si le cooldown de saut est termin�
        if (jumpCooldownTimer <= 0f) {
            // V�rifie si l'entr�e de saut est effectu�e et si le joueur est au sol
            if (context.performed && toucheSol) {
                // V�rifie si la v�locit� verticale actuelle est inf�rieure � la v�locit� maximale
                if (Mathf.Abs(joueurRigidBody.velocity.y) < maxJumpVelocity) {
                    // Applique une force de saut uniquement si la v�locit� verticale est en dessous de la limite
                    joueurRigidBody.AddForce(Vector3.up * forceSaut, ForceMode.Impulse);

                    // Active le cooldown de saut
                    jumpCooldownTimer = jumpCooldownDuration;
                }
            }
        }
    }

    // M�thode pour g�rer le sprint du joueur
    // M�thode appel�e lorsque le sprint commence
public void DebutSprint(InputAction.CallbackContext context) {
    joueurSprint = true; // Marquer que le joueur sprinte
    // Augmenter moveSpeed uniquement si le joueur est au sol ou s'il sprinte d�j� dans les airs
    if (toucheSol || joueurSprint) {
        if (scriptJoueur != null) {
            scriptJoueur.vitesseDep += 2; // moveSpeed + 2
        }
    }
}

    // M�thode appel�e lorsque le sprint se termine
    public void SprintCanceled(InputAction.CallbackContext context) {
        joueurSprint = false; // Marquer que le joueur a arr�t� de sprinter
       // Debug.Log("Sprint Fin");

        if (scriptJoueur != null && toucheSol) { 
                scriptJoueur.vitesseDep = mouvementOrigine; //retourne a sa valeur initial
        }
    }

    // M�thode pour obtenir le vecteur de mouvement normalis� du joueur
    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }
}