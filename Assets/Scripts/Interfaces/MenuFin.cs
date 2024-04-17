using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFin : MonoBehaviour
{
    //variable prenant le parant des deux boutons
    public GameObject boutons;
    
    //Fonction qui retourne au menu du d�but
    public void RetourMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    //fonction pour arr�terle jeu
    public void Quitter()
    {
        Application.Quit();
    }

    //fonction pour faire apparaitre les boutons
    public void BoutonsVisibles()
    {
        boutons.SetActive(true);
    }
}
