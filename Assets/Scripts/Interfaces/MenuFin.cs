using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFin : MonoBehaviour
{
    //variable prenant le parant des deux boutons
    public GameObject boutons;
    public GameObject lumiere;
    
    //Fonction qui retourne au menu du début
    public void RetourMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    //fonction pour arrêterle jeu
    public void Quitter()
    {
        Application.Quit();
    }

    public void LumiereVisible()
    {
        lumiere.SetActive(true);
    }

    //fonction pour faire apparaitre les boutons
    public void BoutonsVisibles()
    {
        boutons.SetActive(true);
    }
}
