using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Change la sc�ne au d�sert
public class ChangeSceneMetropole : MonoBehaviour
{
    // Nom de la sc�ne � charger
    public string sceneNom;

    private void OnTriggerEnter(Collider other)
    {
        // V�rifie si le joueur entre dans le trigger
        if (other.CompareTag("Player"))
        {
            // Charge la sc�ne sp�cifi�e
            SceneManager.LoadScene(sceneNom);
        }
    }
}
