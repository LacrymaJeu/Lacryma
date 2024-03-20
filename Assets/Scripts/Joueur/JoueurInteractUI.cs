using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractionUI : MonoBehaviour
{
    [SerializeField] private GameObject conteneurGameObject;
    [SerializeField] private PlayerInteract joueurInteract;

    private void Update() {
        if (joueurInteract.GetInterationObject() != null){
            Apparait();
            Debug.Log("wesh");
        } else {
            Disparait();
            Debug.Log("non");
        }
    }

    private void Apparait() {
        conteneurGameObject.SetActive(true);
    }

    private void Disparait() {
        conteneurGameObject.SetActive(false);
    }
}
