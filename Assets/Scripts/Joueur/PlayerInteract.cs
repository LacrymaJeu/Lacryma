using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet au joueur d'interagir avec des objets dans son environnement en appuyant sur la touche E,
// activant différentes actions en fonction des composants attachés aux objets détectés.

public class PlayerInteract : MonoBehaviour
{
    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            // distance des collider avec lequel le jouer peut interagir
            float interactRange = 2f;

            // interraction avec tout les collider autour du joueur
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);

            // cherche tout les collider interagis
            foreach (Collider collider in colliderArray) {
                // Vérifier si le collider possède un composant SwitchInteract
                if (collider.TryGetComponent(out SwitchInteract switchInteractable)) {
                    // Appeler la méthode Interact de l'objet SwitchInteract
                    switchInteractable.Interact();
                }

                if (collider.TryGetComponent(out SwitchInteractTemps switchInteractableTemps)) {
                    // Appeler la méthode Interact de l'objet SwitchInteract
                    switchInteractableTemps.Interact();
                }

                if (collider.TryGetComponent(out DeclencheurDialogue npcInteraction)) {
                    // Appeler la méthode Interact de l'objet SwitchInteract
                    npcInteraction.Interact();
                }
            }
        }
    }
    public DeclencheurDialogue GetInterationObject() {
     
            // distance des collider avec lequel le jouer peut interagir
            float interactRange = 2f;

            // interraction avec tout les collider autour du joueur
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);

            // cherche tout les collider interagis
            foreach (Collider collider in colliderArray) {
                // Vérifier si le collider possède un composant SwitchInteract
                if (collider.TryGetComponent(out DeclencheurDialogue npcInteraction)) {
                    return npcInteraction;
                }
        }
        
        return null;
    }
    public SwitchInteractTemps GetInterationSwitch() {
        // distance des colliders avec lesquels le joueur peut interagir
        float interactRange = 2f;

        // Interaction avec tous les colliders autour du joueur
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);

        // Chercher tous les colliders interactifs
        foreach (Collider collider in colliderArray) {
            // Vérifier si le collider possède un composant SwitchInteractTemps
            if (collider.TryGetComponent(out SwitchInteractTemps switchInteractableTemps)) {
                // Retourner l'objet SwitchInteractTemps dès qu'on en trouve un
                return switchInteractableTemps;
            }
        }

        // Retourner null s'il n'y a pas d'objet SwitchInteractTemps à portée
        return null;
    }

}
