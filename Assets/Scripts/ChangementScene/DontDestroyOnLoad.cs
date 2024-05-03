using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Empêche la destruction de l'objet au changement de scène.

public class DontDestroyOnLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
