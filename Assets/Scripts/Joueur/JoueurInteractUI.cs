using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractionUI : MonoBehaviour
{
    [SerializeField] private GameObject conteneurGameObject;
    [SerializeField] private PlayerInteract joueurInteract;

    private void Update() {
        if (joueurInteract.GetInterationObject() || joueurInteract.GetInterationSwitch() != null){
            Apparait();
        } else {
            Disparait();
        }
    }

    private void Apparait() {
        conteneurGameObject.SetActive(true);
    }

    private void Disparait() {
        conteneurGameObject.SetActive(false);
    }
}
