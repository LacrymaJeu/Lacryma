using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Décide si un objet dans la scène peut être ramasser par le joueur.
// Si le script est sur un objet le joueur peut le ramasser.

public class ObjetTenable : MonoBehaviour {


    private Rigidbody objetRigidbody;
    private Transform objetPrendrePointTransformation;

    private void Awake() {
        objetRigidbody = GetComponent<Rigidbody>();
    }

    public void Prendre(Transform objetPrendrePointTransformation) {
        this.objetPrendrePointTransformation = objetPrendrePointTransformation;
        objetRigidbody.useGravity = false;
    }

    public void Lacher() {
        this.objetPrendrePointTransformation = null;
        objetRigidbody.useGravity = true;
    }

    private void FixedUpdate() {
        if (objetPrendrePointTransformation != null) {
            float lerpVitesse = 10f;
            Vector3 nouvellePosition = Vector3.Lerp(transform.position, objetPrendrePointTransformation.position, Time.deltaTime * lerpVitesse);
            objetRigidbody.MovePosition(nouvellePosition);
        }
    }


}