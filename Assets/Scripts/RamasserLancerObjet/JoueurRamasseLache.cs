using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet le joueur de ramasser des objets dans la scène

public class JoueurRamasseLache : MonoBehaviour
{


    [SerializeField] private Transform joueurCameraTransform;
    [SerializeField] private Transform objetDistancePourRamasser;
    [SerializeField] private LayerMask masquePourRamasser;

    private ObjetTenable objetTenable;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (objetTenable == null)
            {
                // Not carrying an object, try to grab
                float ramasseDistance = 4f;
                if (Physics.Raycast(joueurCameraTransform.position, joueurCameraTransform.forward, out RaycastHit raycastFrapper, ramasseDistance, masquePourRamasser))
                {
                    Debug.Log(raycastFrapper.transform);
                    if (raycastFrapper.transform.TryGetComponent(out objetTenable))
                    {
                        objetTenable.Prendre(objetDistancePourRamasser);
                    }
                }
            }
            else
            {
                // Currently carrying something, drop
                objetTenable.Lacher();
                objetTenable = null;
            }
        }
    }
}