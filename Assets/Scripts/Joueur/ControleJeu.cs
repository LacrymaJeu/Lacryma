using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Contr�le la logique du Player Input System

public class ControleJeu : MonoBehaviour {
    // Force de saut
    [SerializeField] private float forceSaut = 5f;

    // Distance de v�rification pour d�terminer si le joueur est au sol
    [SerializeField] private float distanceSol = 0.1f;

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

    // M�thode appel�e au d�marrage de l'objet
    private void Awake() {
        // Initialisation du gestionnaire d'actions du joueur
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        // R�cup�ration du composant Rigidbody attach� � cet objet
        joueurRigidBody = GetComponent<Rigidbody>();

        // Associer la m�thode SprintStarted � l'action de sprint (au d�but de la pression)
        playerInputActions.Player.Sprint.started += SprintDebute;
        // Associer la m�thode SprintCanceled � l'action de sprint (� la fin de la pression)
        playerInputActions.Player.Sprint.canceled += SprintCanceler;

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

    // M�thode appel�e � chaque frame physique
    private void FixedUpdate() {
        // Effectuer une v�rification au sol
        toucheSol = SiToucheSol();
    }

    // V�rifie si le joueur est au sol
    private bool SiToucheSol() {
        // Lance un rayon vers le bas pour v�rifier si le joueur est au sol si le rayon touche le sol cela veut dire que le joueur touche le sol
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, distanceSol)) {
            return true; // Le joueur est au sol si le rayon touche un collider dans groundCheckDistance
        }
        //le rayon ne touche rien le joueur n'est pas au sol
        return false;
    }

    // M�thode pour g�rer le saut du joueur
    public void Saut(InputAction.CallbackContext context) {
        // V�rifie si l'entr�e de saut est effectu�e et si le joueur est au sol
        if (context.performed && toucheSol) {
            // Debug.Log("saut");
            // force pour faire sauter
            joueurRigidBody.AddForce(Vector3.up * forceSaut, ForceMode.Impulse);
        }
    }

    // M�thode pour g�rer le sprint du joueur
    // M�thode appel�e lorsque le sprint commence
    public void SprintDebute(InputAction.CallbackContext context) {
        joueurSprint = true; // Marquer que le joueur sprinte
        Debug.Log("D�but sprint");
        // Increase moveSpeed using the player script reference
        if (toucheSol && scriptJoueur != null) {
                scriptJoueur.vitesseDeplacement += 2; // moveSpeed +2
            
        }
    }
    // M�thode appel�e lorsque le sprint se termine
    public void SprintCanceler(InputAction.CallbackContext context) {
        joueurSprint = false; // Marquer que le joueur a arr�t� de sprinter
        Debug.Log("Sprint Fin");

        if (scriptJoueur != null && toucheSol) { 
                scriptJoueur.vitesseDeplacement = mouvementOrigine; //retourne a sa valeur initial
        }
    }

    // M�thode pour obtenir le vecteur de mouvement normalis� du joueur
    public Vector2 ObtenirVecteurMouvementNormaliser() {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }
}