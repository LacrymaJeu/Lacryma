using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// G�re l'affichage dynamique d'une interface utilisateur pour l'interaction avec des PNJ en fonction des actions du joueur dans le jeu.

public class NPCInteractionUI : MonoBehaviour
{
    [SerializeField] private GameObject conteneurGameObject; // R�f�rence au GameObject contenant l'UI de l'interaction avec le NPC
    [SerializeField] private PlayerInteract joueurInteract; // R�f�rence au script de l'interaction du joueur avec le NPC

    private void Update() {
        // V�rifie si le joueur est en train d'interagir avec un objet ou un interrupteur
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
    // D�sactive le GameObject de l'UI d'interaction
    private void Disparait() {
        conteneurGameObject.SetActive(false);
    }
}
