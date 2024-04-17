using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetRequis : MonoBehaviour
{
    public GameObject objetDesBandits; // Objet que les bandits veulent
    public GameObject murInvisible; // Mur invisible à faire disparaître

    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet qui rentre dans le collider est bien celui recherché
        if (other.gameObject == objetDesBandits)
        {
            Debug.Log("L'objet des bandits est entré dans le trigger.");
            // Fait disparaître le mur invisible
            if (murInvisible != null)
            {
                murInvisible.SetActive(false);
                Debug.Log("Le mur invisible a été désactivé.");
            }
        }
    }
}
