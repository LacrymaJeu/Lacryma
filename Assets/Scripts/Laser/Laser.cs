using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Créer les lasers dans la scène. Quand le joueur le touche il meurt

public class Laser : MonoBehaviour
{

    [SerializeField] private LineRenderer renduLigne; // Visualise laser
    [SerializeField] private float laserDistance = 8f; // Distance maximale du laser
    [SerializeField] private LayerMask ignoreMasque; // Ignorer au lancement du laser
    [SerializeField] private UnityEvent cibleToucher; // Lorsque le laser touche une cible

    private RaycastHit rayFrapper;
    private Ray ray;

    private void Start()
    {
        renduLigne.positionCount = 2; // Point de départ et fin
    }

    private void Update()
    {
        ray = new(transform.position, transform.forward);

        // Vérifie si sa touche quelque chose qui n'est pas ignoré
        if (Physics.Raycast(ray, out rayFrapper, laserDistance, ~ignoreMasque))
        {
            // Visualiser le laser qui touche un objet
            renduLigne.SetPosition(0, transform.position);
            renduLigne.SetPosition(1, rayFrapper.point);
            if (rayFrapper.collider.TryGetComponent(out LaserCible cible))
            {
                cible.Toucher(); // Appelle la fonction de l'autre script
                cibleToucher?.Invoke(); 
            }
        }
        // Visualiser le laser qui atteint sa distance maximale
        else
        {
            renduLigne.SetPosition(0, transform.position);
            renduLigne.SetPosition(1, transform.position + transform.forward * laserDistance);
        }
    }

    // Dessine les Gizmos dans la scène quand nécessaire
    private void OnDrawGizmos()
    {
        // Dessine un rayon rouge 
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray.origin, ray.direction * laserDistance);
        
        // Dessine une sphère bleu à l'endroit ou un objet est touché 
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(rayFrapper.point, 0.23f);
    }

}
