using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneMetropole : MonoBehaviour
{
    public GameObject player; // Variable pour stocker le joueur

    private void OnTriggerEnter(Collider other)
    {
        // V�rifie si le joueur entre dans le trigger
        if (other.gameObject == player)
        {
            // Charge la sc�ne sp�cifi�e
            SceneManager.LoadScene(5);
        }
    }
}
