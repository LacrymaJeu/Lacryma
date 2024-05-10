using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gère l'interaction avec des boîtes animées en inversant leur état de déplacement lorsqu'elles sont interagies.
public class SwitchInteract : MonoBehaviour {
    // Liste des boîtes à animer
    [SerializeField] private List<GameObject> boxesToAnimate = new List<GameObject>();

    // Liste des Animator des boîtes
    private List<Animator> boxAnimators = new List<Animator>();

    // Variable pour suivre l'état de déplacement des boîtes
    private bool moved;

    // Constante pour le nom du paramètre de l'Animator
    private const string HAS_MOVED = "HasMoved";

    // Méthode appelée au démarrage de l'objet
    private void Awake() {
        // Récupération des Animator attachés aux boîtes
        foreach (GameObject box in boxesToAnimate) {
            Animator animator = box.GetComponent<Animator>();
            if (animator != null) {
                boxAnimators.Add(animator);
            }
        }
    }

    // Méthode publique appelée lors de l'interaction avec les boîtes
    public void Interact() {
        // Inversion de l'état de déplacement
        moved = !moved;

        boxAnimators[0].SetTrigger("Pressed");
        // Mise à jour de l'état de toutes les boîtes
        foreach (Animator animator in boxAnimators) {
            if (animator != null) {
             
                animator.SetBool(HAS_MOVED, moved);
            }
        }
    }
}
