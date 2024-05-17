using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterJoueur : MonoBehaviour
{
    // Variable pour d�finir l'objet de destination
    public GameObject destinationTeleportation;

    // Variable pour d�finir l'objet joueur
    public GameObject joueur;

    // Variable pour d�finir l'objet visuel du joueur (pour la d�tection du trigger)
    public GameObject joueurVisuel;

    // V�rifie si l'objet entrant est le joueur visuel d�fini et t�l�porte le joueur
    private void OnTriggerEnter(Collider other)
    {
        // V�rifie si l'objet entrant est le joueur visuel d�fini
        if (other.gameObject == joueurVisuel)
        {
            // Si la destination de t�l�portation est d�finie
            if (destinationTeleportation != null)
            {
                // T�l�porte le joueur � la position de l'objet de destination
                joueur.transform.position = destinationTeleportation.transform.position;
            }
            else
            {
                Debug.LogWarning("La destination de t�l�portation n'est pas d�finie.");
            }
        }
    }
}
