using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetRequis : MonoBehaviour
{
    public GameObject objetDesBandits; // Objet que les bandits veulent
    public GameObject murInvisible; // Mur invisible � faire dispara�tre

    private void OnTriggerEnter(Collider other)
    {
        // V�rifie si l'objet qui rentre dans le collider est bien celui recherch�
        if (other.gameObject == objetDesBandits)
        {
            Debug.Log("L'objet des bandits est entr� dans le trigger.");
            // Fait dispara�tre le mur invisible
            if (murInvisible != null)
            {
                murInvisible.SetActive(false);
                Debug.Log("Le mur invisible a �t� d�sactiv�.");
            }
        }
    }
}
