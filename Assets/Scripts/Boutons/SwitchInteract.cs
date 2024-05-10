using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// G�re l'interaction avec des bo�tes anim�es en inversant leur �tat de d�placement lorsqu'elles sont interagies.
public class SwitchInteract : MonoBehaviour {
    // Liste des bo�tes � animer
    [SerializeField] private List<GameObject> boxesToAnimate = new List<GameObject>();

    // Liste des Animator des bo�tes
    private List<Animator> boxAnimators = new List<Animator>();

    // Variable pour suivre l'�tat de d�placement des bo�tes
    private bool moved;

    // Constante pour le nom du param�tre de l'Animator
    private const string HAS_MOVED = "HasMoved";

    // M�thode appel�e au d�marrage de l'objet
    private void Awake() {
        // R�cup�ration des Animator attach�s aux bo�tes
        foreach (GameObject box in boxesToAnimate) {
            Animator animator = box.GetComponent<Animator>();
            if (animator != null) {
                boxAnimators.Add(animator);
            }
        }
    }

    // M�thode publique appel�e lors de l'interaction avec les bo�tes
    public void Interact() {
        // Inversion de l'�tat de d�placement
        moved = !moved;

        boxAnimators[0].SetTrigger("Pressed");
        // Mise � jour de l'�tat de toutes les bo�tes
        foreach (Animator animator in boxAnimators) {
            if (animator != null) {
             
                animator.SetBool(HAS_MOVED, moved);
            }
        }
    }
}
