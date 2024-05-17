using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterJoueur : MonoBehaviour
{
    // Variable pour définir l'objet de destination
    public GameObject destinationTeleportation;

    // Variable pour définir l'objet joueur
    public GameObject joueur;

    // Variable pour définir l'objet visuel du joueur (pour la détection du trigger)
    public GameObject joueurVisuel;

    // Vérifie si l'objet entrant est le joueur visuel défini et téléporte le joueur
    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet entrant est le joueur visuel défini
        if (other.gameObject == joueurVisuel)
        {
            // Si la destination de téléportation est définie
            if (destinationTeleportation != null)
            {
                // Téléporte le joueur à la position de l'objet de destination
                joueur.transform.position = destinationTeleportation.transform.position;
            }
            else
            {
                Debug.LogWarning("La destination de téléportation n'est pas définie.");
            }
        }
    }
}
