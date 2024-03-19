using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Décide si un objet dans la scène peut être ramasser par le joueur.
// Si le script est sur un objet, le joueur peut le ramasser.

public class ObjetTenable : MonoBehaviour {

    // Référence au Rigidbody de l'objet
    private Rigidbody objetRigidbody;
    // Point de transformation pour prendre l'objet
    private Transform objetPrendrePointTransformation;

    private void Awake() {
        // Initialise le Rigidbody en récupérant le composant de l'objet
        objetRigidbody = GetComponent<Rigidbody>();
    }

    // Fonction appelée pour prendre l'objet
    public void Prendre(Transform objetPrendrePointTransformation) {
        // Définit le point de transformation pour prendre l'objet
        this.objetPrendrePointTransformation = objetPrendrePointTransformation;
        // Désactive la gravité pour que l'objet ne tombe pas
        objetRigidbody.useGravity = false;
    }

    // Fonction appelée pour lâcher l'objet
    public void Lacher() {
        // Réinitialise le point de transformation pour prendre l'objet à null
        this.objetPrendrePointTransformation = null;
        // Réactive la gravité pour que l'objet retombe
        objetRigidbody.useGravity = true;
    }

    private void FixedUpdate() {
        // Vérifie si l'objet est actuellement tenu par le joueur
        if (objetPrendrePointTransformation != null) {
            // Vitesse de l'interpolation linéaire
            float lerpVitesse = 10f;
            // Calcule la nouvelle position en interpolant entre la position actuelle et le point de prise
            Vector3 nouvellePosition = Vector3.Lerp(transform.position, objetPrendrePointTransformation.position, Time.deltaTime * lerpVitesse);
            // Déplace le Rigidbody de l'objet vers la nouvelle position
            objetRigidbody.MovePosition(nouvellePosition);
        }
    }


}