using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet les effets spéciaux de tourner en rond sur eux même

public class TourneEffet : MonoBehaviour
{
    public float rotVitesse_X;
    public float rotVitesse_Y;
    public float rotVitesse_Z;

    public float globalVitesse = 1f;


    void Update()
    {
        transform.Rotate(new Vector3(rotVitesse_X, rotVitesse_Y, rotVitesse_Z) * globalVitesse * Time.deltaTime);
    }
}
