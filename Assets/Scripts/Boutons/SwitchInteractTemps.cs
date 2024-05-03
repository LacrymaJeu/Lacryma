using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gère l'interaction avec les boîtes animées en inversant leur état de déplacement avec un délai d'inversion et un temps de refroidissement après chaque interaction.
public class SwitchInteractTemps : MonoBehaviour {
    // Liste de boîtes à animer
    [SerializeField] private List<GameObject> boxesToAnimate = new List<GameObject>();

    // Liste d'Animator de boîtes
    private List<Animator> boxAnimators = new List<Animator>();

    // Variable pour suivre l'état de déplacement des boîtes
    private bool moved;

    // Constante pour le nom du paramètre de l'Animator
    private const string HAS_MOVED = "HasMoved";

    // Nombre de secondes avant d'inverser l'état moved
    [SerializeField] private float inversionDelay = 10f;

    // Temps pendant lequel le joueur ne peut pas interagir après une interaction
    [SerializeField] private float interactionCooldownTime = 8f;

    // Variable pour suivre l'état de l'interaction
    private bool interactionEnabled = true;

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
        if (!interactionEnabled) {
            return; // Si l'interaction est désactivée, ne pas permettre l'interaction
        }

        // Inversion de l'état de déplacement
        moved = !moved;

        // Mise à jour de l'état de toutes les boîtes
        foreach (Animator animator in boxAnimators) {
            if (animator != null) {
                animator.SetBool(HAS_MOVED, moved);
            }
        }

        // Désactiver l'interaction pendant le délai d'inversion
        interactionEnabled = false;

        // Lancer la coroutine pour inverser moved après inversionDelay secondes
        StartCoroutine(InverserMovedApresDelay());

        // Lancer la coroutine pour activer à nouveau l'interaction après interactionCooldownTime secondes
        StartCoroutine(EnableInteractionApresCooldown());
    }

    // Coroutine pour inverser moved après inversionDelay secondes
    private IEnumerator InverserMovedApresDelay() {
        // Attendre pendant inversionDelay secondes
        yield return new WaitForSeconds(inversionDelay);

        // Inverser moved après le délai
        moved = !moved;

        // Mise à jour de l'état de toutes les boîtes
        foreach (Animator animator in boxAnimators) {
            if (animator != null) {
                animator.SetBool(HAS_MOVED, moved);
            }
        }
    }

    // Coroutine pour activer à nouveau l'interaction après interactionCooldownTime secondes
    private IEnumerator EnableInteractionApresCooldown() {
        // Attendre pendant interactionCooldownTime secondes
        yield return new WaitForSeconds(interactionCooldownTime);

        // Activer à nouveau l'interaction
        interactionEnabled = true;
    }
}
