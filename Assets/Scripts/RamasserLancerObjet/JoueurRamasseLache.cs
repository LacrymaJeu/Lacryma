using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet le joueur de ramasser un objet si il est tenable en pesant "E"

public class JoueurRamasseLache : MonoBehaviour {


    [SerializeField] private Transform joueurCameraTransformer;
    [SerializeField] private Transform objetPointDeTransformation;
    [SerializeField] private LayerMask ramasserLayerMasque; // Les layers

    private ObjetTenable objetTenable;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (objetTenable == null) {
                // Not carrying an object, try to grab
                float ramasserDistance = 4f;
                if (Physics.Raycast(joueurCameraTransformer.position, joueurCameraTransformer.forward, out RaycastHit raycastHit, ramasserDistance, ramasserLayerMasque)) {
                    Debug.Log(raycastHit.transform);
                    if (raycastHit.transform.TryGetComponent(out objetTenable)) {
                        objetTenable.Prendre(objetPointDeTransformation);
                    }
                }
            } else {
                // Currently carrying something, drop
                objetTenable.Lache();
                objetTenable = null;
            }
        }
    }
}