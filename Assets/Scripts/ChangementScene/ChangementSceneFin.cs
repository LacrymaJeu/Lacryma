using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangementSceneFin : MonoBehaviour
{
    void OnEnable()
    {
        // chargeer la derni�re sc�ne
        SceneManager.LoadScene(4);
    }
}
