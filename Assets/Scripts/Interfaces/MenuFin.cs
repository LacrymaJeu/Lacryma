using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFin : MonoBehaviour
{
    public void RetourMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void Quitter()
    {
        Application.Quit();
    }
}
