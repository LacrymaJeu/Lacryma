using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Change la scène au désert
public class ChangeSceneMetropole : MonoBehaviour
{
    // Nom de la scène à charger
    public string sceneNom;

    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si le joueur entre dans le trigger
        if (other.CompareTag("Player"))
        {
            // Charge la scène spécifiée
            SceneManager.LoadScene(sceneNom);
        }
    }
}
