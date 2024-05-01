using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// G�re les actions: retour au menu principal, la fermeture du jeu, l'affichage des boutons et la visibilit� d'une lumi�re dans un menu de fin de jeu.

public class MenuFin : MonoBehaviour
{
    //variable prenant le parant des deux boutons
    public GameObject boutons;
    public GameObject lumiere;
    public GameObject credits;
    
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

    // Rend la lumi�re visible
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
