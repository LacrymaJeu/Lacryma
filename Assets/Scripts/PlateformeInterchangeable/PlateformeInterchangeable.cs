using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet certains objets dans le décors d'apparaître et de disparaître 

public class PlateformeInterchangeable : MonoBehaviour
{
    public KeyCode toggleCle = KeyCode.Q;
    public GameObject[] visibleObjects;
    public GameObject[] invisibleObjects;

    private bool Visible = true;

    void Start()
    {
        SetObjectsVisibility();
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleCle))
        {
            Visible = !Visible;
            SetObjectsVisibility();
        }
    }

    void SetObjectsVisibility()
    {
        foreach (GameObject obj in visibleObjects)
        {
            obj.SetActive(Visible);
        }

        foreach (GameObject obj in invisibleObjects)
        {
            obj.SetActive(!Visible);
        }
    }
}
