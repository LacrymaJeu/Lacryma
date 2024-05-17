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

    private void Awake() {
        animator = GetComponent<Animator>();
        controleJeu = GetComponent<ControleJeu>();
        dernierePosition = player.transform.position;
    }

    private void Update() {
        bool estEnMouvement = player.IlMarche();
        bool estEnCours = player.IlCours() && estEnMouvement;

        animator.SetBool(IL_MARCHE, estEnMouvement);
        animator.SetBool(IL_COURS, estEnCours); // Mettre à jour l'animation de course

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

}
