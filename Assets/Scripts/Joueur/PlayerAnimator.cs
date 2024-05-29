using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gère les animations du joueur en fonction de ses actions telles que la marche, la course et le saut, en utilisant un contrôle de jeu et une instance de Player.

public class PlayerAnimator : MonoBehaviour {

    private const string IL_MARCHE = "IlMarche";
    private const string IL_COURS = "IlCours";
    private const string IDLE = "Idle";
    private const string DEBUT_SAUT = "IlSaute";
    private const string MILIEU_SAUT = "IlTombe";
    private const string FIN_SAUT = "IlTouche";

    private Animator animator;
    [SerializeField] private Player player;
    private ControleJeu controleJeu;

    private Vector3 dernierePosition;

    // Audio variables
    private AudioSource audioSource;
    [SerializeField] private List<AudioClip> marcheSounds;
    private Coroutine soundCoroutine;
    private float marcheDelay = 0.425f;
    private float courseDelay = 0.35f;
    private float currentDelay;

    private void Awake() {
        animator = GetComponent<Animator>();
        controleJeu = GetComponent<ControleJeu>();
        dernierePosition = player.transform.position;
        audioSource = GetComponent<AudioSource>(); // Assurez-vous qu'un AudioSource est attaché à l'objet du joueur
    }

    private void Update() {
        bool estEnMouvement = player.IlMarche();
        bool estEnCours = player.IlCours() && estEnMouvement;

        animator.SetBool(IL_MARCHE, estEnMouvement);
        animator.SetBool(IL_COURS, estEnCours); // Mettre à jour l'animation de course

        // Gérer les sons de marche et de course
        if (estEnCours && (soundCoroutine == null || currentDelay != courseDelay)) {
            if (soundCoroutine != null) {
                StopCoroutine(soundCoroutine);
            }
            soundCoroutine = StartCoroutine(PlaySounds(courseDelay));
            currentDelay = courseDelay;
        } else if (estEnMouvement && !estEnCours && (soundCoroutine == null || currentDelay != marcheDelay)) {
            if (soundCoroutine != null) {
                StopCoroutine(soundCoroutine);
            }
            soundCoroutine = StartCoroutine(PlaySounds(marcheDelay));
            currentDelay = marcheDelay;
        } else if (!estEnMouvement && soundCoroutine != null) {
            StopCoroutine(soundCoroutine);
            soundCoroutine = null;
        }

        // Gérer les animations de saut
        if (controleJeu == null) {
            controleJeu = player.GetComponent<ControleJeu>(); // Assurez-vous d'obtenir la référence correctement
        }

        if (controleJeu != null) {
            // Activer l'animation de début de saut lorsque le joueur commence à sauter
            animator.SetBool(DEBUT_SAUT, controleJeu.IlSaute());

            // Activer l'animation de fin de saut lorsque le joueur touche le sol
            animator.SetBool(FIN_SAUT, controleJeu.ToucheSol());
        }
    }

    private IEnumerator PlaySounds(float delay) {
        while (true) {
            // Vérifier si le joueur est au sol avant de jouer le son
            if (controleJeu != null && controleJeu.ToucheSol()) {
                if (audioSource != null && marcheSounds.Count > 0) {
                    int index = Random.Range(0, marcheSounds.Count);
                    audioSource.PlayOneShot(marcheSounds[index]);
                }
            }
            yield return new WaitForSeconds(delay);
        }
    }
}
