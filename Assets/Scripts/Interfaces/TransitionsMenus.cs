using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class TransitionsMenus : MonoBehaviour
{
    // Variables statiques pour stocker les etats des menus
    static GameObject MenuActif;
    static GameObject NouveauMenu;
    static GameObject MenuPrecedent;

    // gameobjects pour les menus et segments d'interface
    public GameObject UIComplet;
    public GameObject MenuPrincipal;
    public GameObject pauseMenu;
    public GameObject UIJeu;
    public GameObject transition;

    // Variable pour stocker l'etat du jeu
    public static bool enPause;
    
    
    // Start is called before the first frame update
    void Start()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 0)
        {
            MenuActif = MenuPrincipal;
        } 
        else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 1 || UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 2 || UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 3)
        {
            MenuActif = GameObject.Find("MenuPause");
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex != 0 && UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex != 5)
        {
            if (enPause)
            {
                ResumeJeu();
            } else
            {
                PauseJeu();
            }
        }   
    }
    
    // Permet de r�sumer le jeu
    public void ResumeJeu()
    {
        DeterminerNouveauMenu(UIJeu);
        TransitionMenu();
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked; // Verrouille le curseur.
        Cursor.visible = false; // Cache le curseur.
        Time.timeScale = 1f;
        enPause = false;
        print("allo");
    }

    // Permet de mettre en pause
    public void PauseJeu()
    {
        DeterminerNouveauMenu(pauseMenu);
        TransitionMenu();
        Cursor.lockState = CursorLockMode.None; // D�verrouille le curseur.
        Cursor.visible = true; // Rend le curseur visible.
        Time.timeScale = 0f;
        enPause = true;
    }

    // change la scene active
    public void ChangementScene(int Scene)
    {
        Time.timeScale = 1f;
        if (Scene > 0 && Scene < 5)
        {
            NouveauMenu = UIJeu;
        }
        else if (Scene == 0 || Scene == 5)
        {
            NouveauMenu = MenuPrincipal;
            Destroy(UIComplet);
        }
        TransitionMenu();
        // if(Scene == 5)
        // {
        //     Destroy(UIComplet);
        // }
        UnityEngine.SceneManagement.SceneManager.LoadScene(Scene);
        enPause = false;
    }

    // Determine le menu a activer
    public void DeterminerNouveauMenu(GameObject CibleMenu)
    {
        NouveauMenu = CibleMenu;
    }

    // Transition entre les menus
    public void TransitionMenu()
    {   
        if (MenuActif != null)
        {
            MenuActif.SetActive(false);
        }
        if (NouveauMenu != null)
        {
            NouveauMenu.SetActive(true);
            MenuPrecedent = MenuActif;
            MenuActif = NouveauMenu;
        }
    }
    // Retour au menu precedent
    public void RetourMenuPrecedent()
    {
        DeterminerNouveauMenu(MenuPrecedent);
    }

    // Desactive la transition apres qu'elle soit terminee
    public void DesactiverTransition()
    {
        transition.SetActive(false);
    }      

    // Quitter le jeu au complet
        public void QuitJeu()
    {
        Application.Quit();
    }
}
