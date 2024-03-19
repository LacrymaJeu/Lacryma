using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet le joueur de ramasser des objets dans la scène

public class JoueurRamasseLache : MonoBehaviour
{

    // Référence à la transform de la caméra du joueur
    [SerializeField] private Transform joueurCameraTransform;
    // Référence à la transform de l'objet à une distance permettant de le ramasser
    [SerializeField] private Transform objetDistancePourRamasser;
    // Masque utilisé pour filtrer les objets pouvant être ramassés
    [SerializeField] private LayerMask masquePourRamasser;

    // Référence à l'objet actuellement tenu par le joueur
    private ObjetTenable objetTenable;

    private void Update()
    {
        // Vérifie si la touche E est pesé
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Si aucun objet n'est tenu
            if (objetTenable == null)
            {
                // Distance maximale à laquelle le joueur peut ramasser un objet
                float ramasseDistance = 4f;
                // Lance un rayon depuis la position de la caméra du joueur vers l'avant
                if (Physics.Raycast(joueurCameraTransform.position, joueurCameraTransform.forward, out RaycastHit raycastFrapper, ramasseDistance, masquePourRamasser))
                {
                    Debug.Log(raycastFrapper.transform);
                    // Vérifie si l'objet touché possède le composant ObjetTenable
                    if (raycastFrapper.transform.TryGetComponent(out objetTenable))
                    {
                        // Fait appel à la fonction Prendre de l'autre script ObjetTenable pour le ramasser
                        objetTenable.Prendre(objetDistancePourRamasser);
                    }
                }
            }
            else
            {
                // Si un objet est déjà tenu, le joueur le lâche
                objetTenable.Lacher();
                objetTenable = null;
            }
        }
    }
}