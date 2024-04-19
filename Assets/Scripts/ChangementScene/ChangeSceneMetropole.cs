using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneMetropole : MonoBehaviour
{
    public GameObject player; // Variable pour stocker le joueur

    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si le joueur entre dans le trigger
        if (other.gameObject == player)
        {
            // Charge la scène spécifiée
            SceneManager.LoadScene(5);
        }
    }
}
