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

    public player scriptJoueur;

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

    // Méthode appelée au démarrage de l'objet
    private void Awake() {
        // Initialisation du gestionnaire d'actions du joueur
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        // Récupération du composant Rigidbody attaché à cet objet
        joueurRigidBody = GetComponent<Rigidbody>();

        // Associer la méthode SprintStarted à l'action de sprint (au début de la pression)
        playerInputActions.Player.Sprint.started += SprintStarted;
        // Associer la méthode SprintCanceled à l'action de sprint (à la fin de la pression)
        playerInputActions.Player.Sprint.canceled += SprintCanceled;

        //movement original
        mouvementOrigine = scriptJoueur.vitesseDeplacement;
    }


    private void Update() {
        // regard si le joueur touche le sol apres le sprint
        if (scriptJoueur != null && !joueurSprint && !touchaitSol && toucheSol) {
            // revien au movement original quand le joueur touche le sol
            scriptJoueur.vitesseDeplacement = mouvementOrigine;
        }

        // Update the flag indicating whether the player was grounded in the previous frame
        touchaitSol = toucheSol;
    }

    // Méthode appelée à chaque frame physique
    private void FixedUpdate() {
        // Effectuer une vérification au sol
        toucheSol = GroundCheck();
    }

    // Vérifie si le joueur est au sol
    private bool GroundCheck() {
        // Lance un rayon vers le bas pour vérifier si le joueur est au sol si le rayon touche le sol cela veut dire que le joueur touche le sol
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance)) {
            return true; // Le joueur est au sol si le rayon touche un collider dans groundCheckDistance
        }
        //le rayon ne touche rien le joueur n'est pas au sol
        return false;
    }

    // Méthode pour gérer le saut du joueur
    public void Jump(InputAction.CallbackContext context) {
        // Vérifie si l'entrée de saut est effectuée et si le joueur est au sol
        if (context.performed && toucheSol) {
            // Debug.Log("jump");
            // force pour faire sauter
            joueurRigidBody.AddForce(Vector3.up * forceSaut, ForceMode.Impulse);
        }
    }

    // Méthode pour gérer le sprint du joueur
    // Méthode appelée lorsque le sprint commence
    public void SprintStarted(InputAction.CallbackContext context) {
        joueurSprint = true; // Marquer que le joueur sprinte
        Debug.Log("Début sprint");
        // Increase moveSpeed using the player script reference
        if (toucheSol && scriptJoueur != null) {
                scriptJoueur.vitesseDeplacement += 2; // moveSpeed +2
            
        }
    }
    // Méthode appelée lorsque le sprint se termine
    public void SprintCanceled(InputAction.CallbackContext context) {
        joueurSprint = false; // Marquer que le joueur a arrêté de sprinter
        Debug.Log("Sprint Fin");

        if (scriptJoueur != null && toucheSol) { 
                scriptJoueur.vitesseDeplacement = mouvementOrigine; //retourne a sa valeur initial
        }
    }

    // Méthode pour obtenir le vecteur de mouvement normalisé du joueur
    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }
}