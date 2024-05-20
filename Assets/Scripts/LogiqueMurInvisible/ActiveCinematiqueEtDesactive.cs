using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Active la cin�matique quand le joueur rentre dans le trigger. Le fais seulement 1 fois. Desactive aussi un element

public class ActiveCinematiqueEtDesactive : MonoBehaviour
{
    // Variables pour stocker le joueur et les objets � activer et d�sactiver
    public GameObject joueur;
    public GameObject objetAActiver;
    public GameObject[] objetsADesactiver;

    // Variable pour s'assurer que l'action n'est ex�cut�e qu'une seule fois
    private bool aActiver = false;

    // M�thode appel�e lorsque quelque chose entre dans le trigger
    private void OnTriggerEnter(Collider other)
    {
        // V�rifie si l'objet entrant est le joueur et si l'action n'a pas encore �t� ex�cut�e
        if (!aActiver && other.gameObject == joueur)
        {
            // Active l'objet sp�cifi�
            if (objetAActiver != null)
            {
                objetAActiver.SetActive(true);
            }

            // D�sactive les objets sp�cifi�s
            foreach (GameObject objet in objetsADesactiver)
            {
                if (objet != null)
                {
                    objet.SetActive(false);
                }
            }

            // Marque l'action comme ex�cut�e
            aActiver = true;
        }
    }
}
