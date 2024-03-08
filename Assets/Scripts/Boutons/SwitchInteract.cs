using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchInteract : MonoBehaviour {
    // R�f�rence � la bo�te � animer
    [SerializeField] private GameObject boxToAnimate;

    // R�f�rence � l'Animator de la bo�te
    private Animator boxAnimator;

    // Variable pour suivre l'�tat de d�placement de la bo�te
    private bool moved;

    // Constante pour le nom du param�tre de l'Animator
    private const string HAS_MOVED = "HasMoved";

    // M�thode appel�e au d�marrage de l'objet
    private void Start() {
        // R�cup�ration de l'Animator attach� � la bo�te
        boxAnimator = boxToAnimate.GetComponent<Animator>();
    }

    // M�thode publique appel�e lors de l'interaction avec la bo�te
    public void Interact() {
        // Inversion de l'�tat de d�placement
        moved = !moved;
        // V�rification si l'Animator existe
        if (boxAnimator != null) {
            // Mise � jour du param�tre bool�en en fonction de l'�tat actuel
            boxAnimator.SetBool(HAS_MOVED, moved);
        }
    }
}

