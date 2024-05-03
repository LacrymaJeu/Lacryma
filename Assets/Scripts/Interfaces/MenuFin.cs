using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gère les actions: retour au menu principal, la fermeture du jeu, l'affichage des boutons et la visibilité d'une lumière dans un menu de fin de jeu.

public class MenuFin : MonoBehaviour
{
    //variable prenant le parant des deux boutons
    public GameObject boutons;
    public GameObject lumiere;
    public GameObject credits;
    
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

    // Rend la lumière visible
    public void LumiereVisible()
    {
        lumiere.SetActive(true);
        credits.SetActive(false);

    }

    //fonction pour faire apparaitre les boutons
    public void BoutonsVisibles()
    {
        boutons.SetActive(true);
    }
}
