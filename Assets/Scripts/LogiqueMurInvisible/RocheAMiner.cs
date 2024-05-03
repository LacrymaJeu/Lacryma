using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// D�sactive les roches pour que le joueur peut passer quand le joueur s'approche du chef dans le d�sert
public class RocheAMiner : MonoBehaviour
{
    // D�clare un tableau pour stocker les objets � d�sactiver
    public GameObject[] objetsADesactiver;

    // Ajoute une variable pour le GameObject du joueur
    public GameObject joueur;

    // Cette fonction est appel�e lorsqu'un autre collider entre dans le trigger
    private void OnTriggerEnter(Collider other)
    {
        // V�rifie si l'objet entrant est le joueur
        if (other.gameObject == joueur)
        {
            // Parcours chaque GameObject dans le tableau et le d�sactive
            foreach (GameObject obj in objetsADesactiver)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                }
            }
        }
    }
}
