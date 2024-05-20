using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Active la cin�matique quand le joueur rentre dans le trigger. Le fais seulement 1 fois

public class ActiveCinematique : MonoBehaviour
{
    // Variables pour stocker le joueur et l'objet � activer
    public GameObject joueur;
    public GameObject objetAActiver;

    // Variable pour s'assurer que l'action n'est ex�cut�e qu'une seule fois
    private bool aActiver = false;

    // M�thode appel�e lorsque quelque chose entre dans le trigger
    private void OnTriggerEnter(Collider other)
    {
        // V�rifie si l'objet entrant est le joueur et si l'action n'a pas encore �t� ex�cut�e
        if (!aActiver && other.gameObject == joueur)
        {
            // Active l'objet sp�cifi�
            objetAActiver.SetActive(true);

            // Marque l'action comme ex�cut�e
            aActiver = true;
        }
    }
}
