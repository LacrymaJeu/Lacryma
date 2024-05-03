using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gère l'affichage dynamique d'une interface utilisateur pour l'interaction avec des PNJ en fonction des actions du joueur dans le jeu.

public class NPCInteractionUI : MonoBehaviour
{
    [SerializeField] private GameObject conteneurGameObject; // Référence au GameObject contenant l'UI de l'interaction avec le NPC
    [SerializeField] private PlayerInteract joueurInteract; // Référence au script de l'interaction du joueur avec le NPC

    private void Update() {
        // Vérifie si le joueur est en train d'interagir avec un objet ou un interrupteur
        if (joueurInteract.GetInterationObject() || joueurInteract.GetInterationSwitch() || joueurInteract.GetInterationSwitch2() != null){
            Apparait();
        } else {
            Disparait();
        }
    }

    // Active le GameObject de l'UI d'interaction
    private void Apparait() {
        conteneurGameObject.SetActive(true);
    }
    // Désactive le GameObject de l'UI d'interaction
    private void Disparait() {
        conteneurGameObject.SetActive(false);
    }
}
