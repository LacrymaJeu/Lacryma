using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Active la cinématique quand le joueur rentre dans le trigger. Le fais seulement 1 fois. Desactive aussi un element

public class ActiveCinematiqueEtDesactive : MonoBehaviour
{
    // Variables pour stocker le joueur et les objets à activer et désactiver
    public GameObject joueur;
    public GameObject objetAActiver;
    public GameObject[] objetsADesactiver;

    // Variable pour s'assurer que l'action n'est exécutée qu'une seule fois
    private bool aActiver = false;

    // Méthode appelée lorsque quelque chose entre dans le trigger
    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet entrant est le joueur et si l'action n'a pas encore été exécutée
        if (!aActiver && other.gameObject == joueur)
        {
            // Active l'objet spécifié
            if (objetAActiver != null)
            {
                objetAActiver.SetActive(true);
            }

            // Désactive les objets spécifiés
            foreach (GameObject objet in objetsADesactiver)
            {
                if (objet != null)
                {
                    objet.SetActive(false);
                }
            }

            // Marque l'action comme exécutée
            aActiver = true;
        }
    }
}
