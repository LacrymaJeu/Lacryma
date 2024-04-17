using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuDepart : MonoBehaviour
{
    public static GameObject MenuActif;
    static GameObject NouveauMenu;
    public Button Options;
    public GameObject MenuPrincipal;

    // Start is called before the first frame update
    void Start()
    {
        MenuActif = MenuPrincipal;
    }
    // Update is called once per frame
    void Update()
    {
    }

    public void DemarrerJeu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
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
