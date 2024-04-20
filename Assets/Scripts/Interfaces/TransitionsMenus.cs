using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionsMenus : MonoBehaviour
{
    public static GameObject MenuActif;
    static GameObject NouveauMenu;
    public GameObject MenuPrincipal;

    // Start is called before the first frame update
    void Start()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 0)
        {
            MenuActif = MenuPrincipal;
        } else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 1 || UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 2 || UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 3)
        {
            MenuActif = GameObject.Find("MenuPause");
        }
        
    }
    // Update is called once per frame
    void Update()
    {
    }

    public void ChangementScene(int Scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(Scene);
    }

    public void DeterminerNouveauMenu(GameObject CibleMenu)
    {
        NouveauMenu = CibleMenu;
    }

    public void TransitionMenu()
    {   
        MenuActif.SetActive(false);
        NouveauMenu.SetActive(true);
        MenuActif = NouveauMenu;
        Invoke("DesactiverTransition", 2);
    }

    public void DesactiverTransition()
    {
        gameObject.SetActive(false);
    }

}
