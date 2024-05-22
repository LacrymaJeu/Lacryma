using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangementSceneFin : MonoBehaviour
{
    void OnEnable()
    {
        // chargeer la dernière scène
        SceneManager.LoadScene(4);
    }
}
