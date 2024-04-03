using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Décide ou la plateforme bougeante va aller quand il bouge en changeant sont transform dans l'inspecteur
// Peut aussi contrôler la rotation du plateforme en bougeant l'axe des Y dans la rotation

public class PointDeRepere : MonoBehaviour
{
    // Obtien le transform de la plateforme à un index spécifique
    public Transform ObtenirPassagePlateforme(int waypointIndex)
    {
        return transform.GetChild(waypointIndex);
    }

    // Obtien l'index de la prochaine plateforme à partir de l'index actuel
    public int ObtenirProchainePassagePlateformeIndex(int currentWaypointIndex)
    {
        int nextWaypointIndex = currentWaypointIndex + 1; // Calcule l'index de la prochaine plateforme

        if (nextWaypointIndex == transform.childCount) // Vérifie si l'index dépasse le nombre total de plateformes
        {
            nextWaypointIndex = 0;
        }

        return nextWaypointIndex; // Retourne l'index de la prochaine plateforme
    }
}