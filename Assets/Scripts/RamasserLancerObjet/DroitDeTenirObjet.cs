using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Active le script qui permet au joueur de tenir des objets

public class DroitDeTenirObjet : MonoBehaviour
{
    public GameObject joueur; // Script � activer sur le joueur
    public GameObject objetADisparaitre; // Objet � faire dispara�tre

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            joueur.GetComponent<JoueurRamasseLache>().enabled = true;
            Debug.Log("Le script sur le joueur a �t� activ�.");

            // Fait dispara�tre l'objet
            Destroy(objetADisparaitre);
            Debug.Log("L'objet a �t� d�truit.");
        }
    }
}
