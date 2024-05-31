using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchInteractTemps : MonoBehaviour {
    [SerializeField] private List<GameObject> boxesToAnimate = new List<GameObject>();
    private bool moved;
    private const string HAS_MOVED = "HasMoved";

    [SerializeField] private float soundIntensity = 1.0f; // Intensité par défaut
    [SerializeField] private float inversionDelay = 10f;
    [SerializeField] private float interactionCooldownTime = 8f;
    private bool interactionEnabled = true;

    [SerializeField] private AudioClip interactionSound;

    private void InteractWithSound(GameObject obj) {
        AudioSource audioSource = obj.GetComponent<AudioSource>();
        if (audioSource != null && interactionSound != null) {
            audioSource.volume = soundIntensity;
            audioSource.PlayOneShot(interactionSound);
        }
    }

    private void Awake() {
        foreach (GameObject box in boxesToAnimate) {
            AudioSource audioSource = box.GetComponent<AudioSource>();
            if (audioSource == null) {
                audioSource = box.AddComponent<AudioSource>();
            }
        }
    }

    public void Interact() {
        if (!interactionEnabled) {
            return;
        }

        Debug.Log("Interaction started");

        foreach (GameObject box in boxesToAnimate) {
            InteractWithSound(box);
        }

        moved = !moved;

        foreach (GameObject box in boxesToAnimate) {
            Animator animator = box.GetComponent<Animator>();
            if (animator != null) {
                animator.SetBool(HAS_MOVED, moved);
            }
        }

        interactionEnabled = false;
        StartCoroutine(InverserMovedApresDelay());
        StartCoroutine(EnableInteractionApresCooldown());
    }

    private IEnumerator InverserMovedApresDelay() {
        yield return new WaitForSeconds(inversionDelay);
        moved = !moved;

        foreach (GameObject box in boxesToAnimate) {
            Animator animator = box.GetComponent<Animator>();
            if (animator != null) {
                animator.SetBool(HAS_MOVED, moved);
            }
        }
    }

    private IEnumerator EnableInteractionApresCooldown() {
        yield return new WaitForSeconds(interactionCooldownTime);

        Debug.Log("Interaction cooldown ended");

        interactionEnabled = true;
    }
}
