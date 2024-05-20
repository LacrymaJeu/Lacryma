using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Active la cinématique quand le joueur rentre dans le trigger. Le fais seulement 1 fois

public class ActiveCinematique : MonoBehaviour
{
    // Variables pour stocker le joueur et l'objet à activer
    public GameObject joueur;
    public GameObject objetAActiver;

    // Variable pour s'assurer que l'action n'est exécutée qu'une seule fois
    private bool aActiver = false;

    // Méthode appelée lorsque quelque chose entre dans le trigger
    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet entrant est le joueur et si l'action n'a pas encore été exécutée
        if (!aActiver && other.gameObject == joueur)
        {
            // Active l'objet spécifié
            objetAActiver.SetActive(true);

            // Marque l'action comme exécutée
            aActiver = true;
        }
    }
}
