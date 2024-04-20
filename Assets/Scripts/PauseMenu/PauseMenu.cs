using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Permet de mettre le jeu en pause

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool enPause;

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
    // Permet de mettre en pause
    public void PauseJeu()
    {
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None; // Déverrouille le curseur.
        Cursor.visible = true; // Rend le curseur visible.
        Time.timeScale = 0f;
        enPause = true;
    }
    // Permet de résumer le jeu
    public void ResumeJeu()
    {
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked; // Verrouille le curseur.
        Cursor.visible = false; // Cache le curseur.
        Time.timeScale = 1f;
        enPause = false;
    }
    // Aller au menu principale du jeu
    public void VaAuMenuPrincipale()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        enPause = false;
    }
    // Quitter le jeu au complet
    public void QuitJeu()
    {
        Application.Quit();
    }
}
