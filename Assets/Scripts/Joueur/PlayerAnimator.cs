using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

    // Noms des paramètres d'animation dans l'Animator
    private const string IL_MARCHE = "IlMarche";
    private const string IL_COURS = "IlCours";
    private const string IDLE = "Idle";
    private const string DEBUT_SAUT = "IlSaute";
    private const string MILIEU_SAUT = "IlTombe";
    private const string FIN_SAUT = "IlToucheSol";

    // Référence à l'Animator
    private Animator animator;

    // Référence au script Player
    [SerializeField] private Player player;

    // Dernière position enregistrée du joueur
    private Vector3 dernierePosition;

    private void Awake() {
        // Initialisation de l'Animator
        animator = GetComponent<Animator>();
        // Enregistrement de la position initiale du joueur
        dernierePosition = player.transform.position;
    }

    private void Update() {
        // Vérifie si le joueur bouge actuellement
        bool estEnMouvement = player.transform.position != dernierePosition;
        // Met à jour la dernière position du joueur
        dernierePosition = player.transform.position;


        // Active l'animation de marche si le joueur bouge
        animator.SetBool(IL_MARCHE, estEnMouvement);

        // Active l'animation de course si le joueur bouge et court
        animator.SetBool(IL_COURS, estEnMouvement && player.IlCours());
     

        // Si le joueur ne bouge pas, jouer l'animation Idle
        if (!estEnMouvement) {
            animator.Play(IDLE);
        }
    }
}