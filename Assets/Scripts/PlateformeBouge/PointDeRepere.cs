using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// D�cide ou la plateforme bougeante va aller quand il bouge en changeant sont transform dans l'inspecteur
// Peut aussi contr�ler la rotation du plateforme en bougeant l'axe des Y dans la rotation

public class PointDeRepere : MonoBehaviour
{
    // Obtien le transform de la plateforme � un index sp�cifique
    public Transform ObtenirPassagePlateforme(int waypointIndex)
    {
        return transform.GetChild(waypointIndex);
    }

    // Obtien l'index de la prochaine plateforme � partir de l'index actuel
    public int ObtenirProchainePassagePlateformeIndex(int currentWaypointIndex)
    {
        int nextWaypointIndex = currentWaypointIndex + 1; // Calcule l'index de la prochaine plateforme

        if (nextWaypointIndex == transform.childCount) // V�rifie si l'index d�passe le nombre total de plateformes
        {
            nextWaypointIndex = 0;
        }

        return nextWaypointIndex; // Retourne l'index de la prochaine plateforme
    }
}