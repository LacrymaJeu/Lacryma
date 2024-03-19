using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// D�cide si un objet dans la sc�ne peut �tre ramasser par le joueur.
// Si le script est sur un objet, le joueur peut le ramasser.

public class ObjetTenable : MonoBehaviour {

    // R�f�rence au Rigidbody de l'objet
    private Rigidbody objetRigidbody;
    // Point de transformation pour prendre l'objet
    private Transform objetPrendrePointTransformation;

    private void Awake() {
        // Initialise le Rigidbody en r�cup�rant le composant de l'objet
        objetRigidbody = GetComponent<Rigidbody>();
    }

    // Fonction appel�e pour prendre l'objet
    public void Prendre(Transform objetPrendrePointTransformation) {
        // D�finit le point de transformation pour prendre l'objet
        this.objetPrendrePointTransformation = objetPrendrePointTransformation;
        // D�sactive la gravit� pour que l'objet ne tombe pas
        objetRigidbody.useGravity = false;
    }

    // Fonction appel�e pour l�cher l'objet
    public void Lacher() {
        // R�initialise le point de transformation pour prendre l'objet � null
        this.objetPrendrePointTransformation = null;
        // R�active la gravit� pour que l'objet retombe
        objetRigidbody.useGravity = true;
    }

    private void FixedUpdate() {
        // V�rifie si l'objet est actuellement tenu par le joueur
        if (objetPrendrePointTransformation != null) {
            // Vitesse de l'interpolation lin�aire
            float lerpVitesse = 10f;
            // Calcule la nouvelle position en interpolant entre la position actuelle et le point de prise
            Vector3 nouvellePosition = Vector3.Lerp(transform.position, objetPrendrePointTransformation.position, Time.deltaTime * lerpVitesse);
            // D�place le Rigidbody de l'objet vers la nouvelle position
            objetRigidbody.MovePosition(nouvellePosition);
        }
    }


}