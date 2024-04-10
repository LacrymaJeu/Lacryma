using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

    private const string IL_MARCHE = "IlMarche";
    private const string IL_COURS = "IlCours";

    private Animator animator;

    [SerializeField] private Player player;

    private void Awake() {
        animator = GetComponent<Animator>();
       
    }

    private void Update() {
        animator.SetBool(IL_MARCHE, player.IlMarche());
        animator.SetBool(IL_COURS, player.IlCours());
    }
}
