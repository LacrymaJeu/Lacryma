using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchInteract : MonoBehaviour {
    [SerializeField] private List<GameObject> boxesToAnimate = new List<GameObject>();
    private List<Animator> boxAnimators = new List<Animator>();
    private bool moved;
    private const string HAS_MOVED = "HasMoved";

    [SerializeField] private float soundIntensity = 1.0f; // Intensité par défaut
    [SerializeField] private AudioClip interactionSound;

    private void Awake() {
        foreach (GameObject box in boxesToAnimate) {
            Animator animator = box.GetComponent<Animator>();
            if (animator != null) {
                boxAnimators.Add(animator);
            }
            // Ajouter un composant AudioSource si nécessaire
            AudioSource audioSource = box.GetComponent<AudioSource>();
            if (audioSource == null) {
                audioSource = box.AddComponent<AudioSource>();
            }
        }
    }

    public void Interact() {
        moved = !moved;

        if (interactionSound != null) {
            foreach (GameObject box in boxesToAnimate) {
                AudioSource audioSource = box.GetComponent<AudioSource>();
                if (audioSource != null) {
                    audioSource.volume = soundIntensity;
                    audioSource.PlayOneShot(interactionSound);
                }
            }
        }

        foreach (Animator animator in boxAnimators) {
            if (animator != null) {
                animator.SetBool(HAS_MOVED, moved);
            }
        }
    }
}
