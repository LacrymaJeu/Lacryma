using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Active le script qui permet au joueur de tenir des objets

public class DroitDeTenirObjet : MonoBehaviour
{
    public GameObject joueur; // Script à activer sur le joueur
    public GameObject objetADisparaitre; // Objet à faire disparaître

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            joueur.GetComponent<JoueurRamasseLache>().enabled = true;
            Debug.Log("Le script sur le joueur a été activé.");

            // Fait disparaître l'objet
            Destroy(objetADisparaitre);
            Debug.Log("L'objet a été détruit.");
        }
    }
}
