using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Emp�che la destruction de l'objet au changement de sc�ne.

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
