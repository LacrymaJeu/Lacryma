using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuDepart : MonoBehaviour
{

    public void DemarrerJeu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
