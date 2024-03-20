using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

                if (collider.TryGetComponent(out NPCInteraction npcInteraction)) {
                    // Appeler la méthode Interact de l'objet SwitchInteract
                    npcInteraction.Interact();
                }
            }
        }
    }
    public NPCInteraction GetInterationObject() {
     
            // distance des collider avec lequel le jouer peut interagir
            float interactRange = 2f;

            // interraction avec tout les collider autour du joueur
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);

            // cherche tout les collider interagis
            foreach (Collider collider in colliderArray) {
                // Vérifier si le collider possède un composant SwitchInteract
                if (collider.TryGetComponent(out NPCInteraction npcInteraction)) {
                    return npcInteraction;
                }
            }
        
        return null;
    }
}
