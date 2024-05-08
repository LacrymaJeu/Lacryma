using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Quand l'objet requis rentre dans la zone du is trigger le mur invisible dispara�t et le joueur peut avancer

public class ObjetRequis : MonoBehaviour
{
    public GameObject objetDesBandits; // Objet que les bandits veulent
    public GameObject[] objetsAFaireDisparaitre; // Tableau des objets � faire dispara�tre

    private void OnTriggerEnter(Collider other)
    {
        // V�rifie si l'objet qui rentre dans le collider est bien celui recherch�
        if (other.gameObject == objetDesBandits)
        {
            Debug.Log("L'objet des bandits est entr� dans le trigger.");
            // Fait dispara�tre tous les objets du tableau
            if (objetsAFaireDisparaitre != null)
            {
                foreach (GameObject objet in objetsAFaireDisparaitre)
                {
                    if (objet != null)
                    {
                        objet.SetActive(false);
                        Debug.Log("Un objet a �t� d�sactiv� : " + objet.name);
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
