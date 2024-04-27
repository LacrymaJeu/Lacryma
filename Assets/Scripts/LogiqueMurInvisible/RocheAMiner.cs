using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Désactive les roches pour que le joueur peut passer quand le joueur s'approche du chef dans le désert
public class RocheAMiner : MonoBehaviour
{
    // Déclare un tableau pour stocker les objets à désactiver
    public GameObject[] objetsADesactiver;

    // Ajoute une variable pour le GameObject du joueur
    public GameObject joueur;

    // Cette fonction est appelée lorsqu'un autre collider entre dans le trigger
    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet entrant est le joueur
        if (other.gameObject == joueur)
        {
            // Parcours chaque GameObject dans le tableau et le désactive
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
