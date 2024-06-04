using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionsMenus : MonoBehaviour {
    // Variables statiques pour stocker les états des menus
    static GameObject MenuActif;
    static GameObject NouveauMenu;
    static GameObject MenuPrecedent;

    // GameObjects pour les menus et segments d'interface
    public GameObject UIComplet;
    public GameObject MenuPrincipal;
    public GameObject pauseMenu;
    public GameObject UIJeu;
    public GameObject transition;

    // Variable pour stocker l'état du jeu
    public static bool enPause;

    // Start est appelée avant la première mise à jour de la frame
    void Start() {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 0) {
            MenuActif = MenuPrincipal;
        } else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 1 || UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 2 || UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 3) {
            MenuActif = GameObject.Find("MenuPause");
        }
    }

    // Update est appelée une fois par frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex != 0 && UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex != 5) {
            if (enPause) {
                ResumeJeu();
            } else {
                PauseJeu();
            }
        }
    }

    // Permet de reprendre le jeu
    public void ResumeJeu() {
        DeterminerNouveauMenu(UIJeu);
        TransitionMenu();
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked; // Verrouille le curseur.
        Cursor.visible = false; // Cache le curseur.
        Time.timeScale = 1f;
        enPause = false;
    }

    // Permet de mettre le jeu en pause
    public void PauseJeu() {
        DeterminerNouveauMenu(pauseMenu);
        TransitionMenu();
        Cursor.lockState = CursorLockMode.None; // Déverrouille le curseur.
        Cursor.visible = true; // Rend le curseur visible.
        Time.timeScale = 0f;
        enPause = true;
    }

    // Change la scène active
    public void ChangementScene(int Scene) {
        Time.timeScale = 1f;
        if (Scene > 0 && Scene < 5) {
            NouveauMenu = UIJeu;
        } else if (Scene == 0 || Scene == 5) {
            NouveauMenu = MenuPrincipal;
            Destroy(UIComplet);
        }
        TransitionMenu();
        UnityEngine.SceneManagement.SceneManager.LoadScene(Scene);
        enPause = false;
    }

    // Détermine le menu à activer
    public void DeterminerNouveauMenu(GameObject CibleMenu) {
        NouveauMenu = CibleMenu;
    }

    // Transition entre les menus
    public void TransitionMenu() {
        if (MenuActif != null) {
            MenuActif.SetActive(false);
        }
        if (NouveauMenu != null) {
            NouveauMenu.SetActive(true);
            MenuPrecedent = MenuActif;
            MenuActif = NouveauMenu;
        }
    }

    // Retourne au menu précédent
    public void RetourMenuPrecedent() {
        DeterminerNouveauMenu(MenuPrecedent);
    }

    // Désactive la transition après qu'elle soit terminée
    public void DesactiverTransition() {
        transition.SetActive(false);
    }

    // Quitte le jeu complètement
    public void QuitJeu() {
        Application.Quit();
    }
}
