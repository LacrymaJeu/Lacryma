using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet les effets sp�ciaux de tourner en rond sur eux m�me

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
