using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet certains objets dans le décors d'apparaître et de disparaître 

public class PlateformeInterchangeable : MonoBehaviour
{
    public KeyCode toggleKey = KeyCode.Q;
    public GameObject[] visibleObjects;
    public GameObject[] invisibleObjects;

    private bool isVisible = true;

    void Start()
    {
        SetObjectsVisibility();
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            isVisible = !isVisible;
            SetObjectsVisibility();
        }
    }

    void SetObjectsVisibility()
    {
        foreach (GameObject obj in visibleObjects)
        {
            obj.SetActive(isVisible);
        }

        foreach (GameObject obj in invisibleObjects)
        {
            obj.SetActive(!isVisible);
        }
    }
}
