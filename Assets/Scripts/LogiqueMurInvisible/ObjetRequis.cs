using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Quand l'objet requis rentre dans la zone du is trigger le mur invisible disparaît et le joueur peut avancer

public class ObjetRequis : MonoBehaviour
{
    public GameObject objetDesBandits; // Objet que les bandits veulent
    public GameObject[] objetsAFaireDisparaitre; // Tableau des objets à faire disparaître

    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet qui rentre dans le collider est bien celui recherché
        if (other.gameObject == objetDesBandits)
        {
            Debug.Log("L'objet des bandits est entré dans le trigger.");
            // Fait disparaître tous les objets du tableau
            if (objetsAFaireDisparaitre != null)
            {
                foreach (GameObject objet in objetsAFaireDisparaitre)
                {
                    if (objet != null)
                    {
                        objet.SetActive(false);
                        Debug.Log("Un objet a été désactivé : " + objet.name);
                        // Activer tous les enfants de chaque objet
                        ActivateChildren(objet, true);
                    }
                }
            }
        }
    }

    private void ActivateChildren(GameObject parent, bool activate)
    {
        foreach (Transform child in parent.transform)
        {
            child.gameObject.SetActive(activate);
        }
    }
}
