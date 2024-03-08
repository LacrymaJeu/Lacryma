using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchInteract : MonoBehaviour {
    // Référence à la boîte à animer
    [SerializeField] private GameObject boxToAnimate;

    // Référence à l'Animator de la boîte
    private Animator boxAnimator;

    // Variable pour suivre l'état de déplacement de la boîte
    private bool moved;

    // Constante pour le nom du paramètre de l'Animator
    private const string HAS_MOVED = "HasMoved";

    // Méthode appelée au démarrage de l'objet
    private void Start() {
        // Récupération de l'Animator attaché à la boîte
        boxAnimator = boxToAnimate.GetComponent<Animator>();
    }

    // Méthode publique appelée lors de l'interaction avec la boîte
    public void Interact() {
        // Inversion de l'état de déplacement
        moved = !moved;
        // Vérification si l'Animator existe
        if (boxAnimator != null) {
            // Mise à jour du paramètre booléen en fonction de l'état actuel
            boxAnimator.SetBool(HAS_MOVED, moved);
        }
    }
}

