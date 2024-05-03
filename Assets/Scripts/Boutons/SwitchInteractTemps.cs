using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// G�re l'interaction avec les bo�tes anim�es en inversant leur �tat de d�placement avec un d�lai d'inversion et un temps de refroidissement apr�s chaque interaction.
public class SwitchInteractTemps : MonoBehaviour {
    // Liste de bo�tes � animer
    [SerializeField] private List<GameObject> boxesToAnimate = new List<GameObject>();

    // Liste d'Animator de bo�tes
    private List<Animator> boxAnimators = new List<Animator>();

    // Variable pour suivre l'�tat de d�placement des bo�tes
    private bool moved;

    // Constante pour le nom du param�tre de l'Animator
    private const string HAS_MOVED = "HasMoved";

    // Nombre de secondes avant d'inverser l'�tat moved
    [SerializeField] private float inversionDelay = 10f;

    // Temps pendant lequel le joueur ne peut pas interagir apr�s une interaction
    [SerializeField] private float interactionCooldownTime = 8f;

    // Variable pour suivre l'�tat de l'interaction
    private bool interactionEnabled = true;

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
        if (!interactionEnabled) {
            return; // Si l'interaction est d�sactiv�e, ne pas permettre l'interaction
        }

        // Inversion de l'�tat de d�placement
        moved = !moved;

        // Mise � jour de l'�tat de toutes les bo�tes
        foreach (Animator animator in boxAnimators) {
            if (animator != null) {
                animator.SetBool(HAS_MOVED, moved);
            }
        }

        // D�sactiver l'interaction pendant le d�lai d'inversion
        interactionEnabled = false;

        // Lancer la coroutine pour inverser moved apr�s inversionDelay secondes
        StartCoroutine(InverserMovedApresDelay());

        // Lancer la coroutine pour activer � nouveau l'interaction apr�s interactionCooldownTime secondes
        StartCoroutine(EnableInteractionApresCooldown());
    }

    // Coroutine pour inverser moved apr�s inversionDelay secondes
    private IEnumerator InverserMovedApresDelay() {
        // Attendre pendant inversionDelay secondes
        yield return new WaitForSeconds(inversionDelay);

        // Inverser moved apr�s le d�lai
        moved = !moved;

        // Mise � jour de l'�tat de toutes les bo�tes
        foreach (Animator animator in boxAnimators) {
            if (animator != null) {
                animator.SetBool(HAS_MOVED, moved);
            }
        }
    }

    // Coroutine pour activer � nouveau l'interaction apr�s interactionCooldownTime secondes
    private IEnumerator EnableInteractionApresCooldown() {
        // Attendre pendant interactionCooldownTime secondes
        yield return new WaitForSeconds(interactionCooldownTime);

        // Activer � nouveau l'interaction
        interactionEnabled = true;
    }
}
