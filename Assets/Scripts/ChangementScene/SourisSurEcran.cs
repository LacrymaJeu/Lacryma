using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Met la souris sur l'�cran

public class SourisSurEcran : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None; // D�verrouille le curseur.
        Cursor.visible = true; // Rend le curseur visible.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
