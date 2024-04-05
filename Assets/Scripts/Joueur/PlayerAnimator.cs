using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator animator;
    bool courtAnim = false;
    public GameObject joueur;

    private void Awake() {
        // Récupération de l'Animator attaché à cet objet
       animator = GetComponent<Animator>();
    }

    // Méthode appelée à chaque frame
    private void Update() {
        if(joueur.GetComponent<Rigidbody>().velocity.magnitude>0.1){
            animator.SetBool("marche", true);

             if((Input.GetKeyDown(KeyCode.LeftShift)) && !courtAnim ){
                courtAnim = true;
                 animator.SetBool("court", true);
             }else if((Input.GetKeyDown(KeyCode.LeftShift)) && courtAnim)
            {
            animator.SetBool("court", false);
              }

        }else if(joueur.GetComponent<Rigidbody>().velocity.magnitude < 0.1)
        {
          animator.SetBool("marche", true);
        }
    }
}
