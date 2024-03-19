using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet le joueur de ramasser des objets dans la sc�ne

public class JoueurRamasseLache : MonoBehaviour
{

    // R�f�rence � la transform de la cam�ra du joueur
    [SerializeField] private Transform joueurCameraTransform;
    // R�f�rence � la transform de l'objet � une distance permettant de le ramasser
    [SerializeField] private Transform objetDistancePourRamasser;
    // Masque utilis� pour filtrer les objets pouvant �tre ramass�s
    [SerializeField] private LayerMask masquePourRamasser;

    // R�f�rence � l'objet actuellement tenu par le joueur
    private ObjetTenable objetTenable;

    private void Update()
    {
        // V�rifie si la touche E est pes�
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Si aucun objet n'est tenu
            if (objetTenable == null)
            {
                // Distance maximale � laquelle le joueur peut ramasser un objet
                float ramasseDistance = 4f;
                // Lance un rayon depuis la position de la cam�ra du joueur vers l'avant
                if (Physics.Raycast(joueurCameraTransform.position, joueurCameraTransform.forward, out RaycastHit raycastFrapper, ramasseDistance, masquePourRamasser))
                {
                    Debug.Log(raycastFrapper.transform);
                    // V�rifie si l'objet touch� poss�de le composant ObjetTenable
                    if (raycastFrapper.transform.TryGetComponent(out objetTenable))
                    {
                        // Fait appel � la fonction Prendre de l'autre script ObjetTenable pour le ramasser
                        objetTenable.Prendre(objetDistancePourRamasser);
                    }
                }
            }
            else
            {
                // Si un objet est d�j� tenu, le joueur le l�che
                objetTenable.Lacher();
                objetTenable = null;
            }
        }
    }
}