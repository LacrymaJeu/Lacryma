using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet au joueur de rester sur les plateformes qui bougent

public class PlateformeBouge : MonoBehaviour
{
    [SerializeField]
    private PointDeRepere _passagePlateforme; // Passage la plateforme prend

    [SerializeField]
    private float _vitesse;

    private int _ciblePassagePlateformeIndex;

    private Transform _precedentPassagePlateforme;
    private Transform _ciblePassagePlateforme;

    private float _tempsAPassagePlateforme;
    private float _tempsEcouler;

    private HashSet<Transform> suitObjets = new HashSet<Transform>(); // HashSet pour suivre les objets sur la plateforme

    void Start()
    {
        CibleProchainPassagePlateforme(); // Appel de la fonction pour d�finir la prochaine cible
    }

    void FixedUpdate()
    {
        _tempsEcouler += Time.deltaTime; // Mise � jour du temps �coul�

        float ecoulerPourcentage = _tempsEcouler / _tempsAPassagePlateforme; // Calcul du pourcentage de d�placement
        ecoulerPourcentage = Mathf.SmoothStep(0, 1, ecoulerPourcentage); // Application d'une interpolation de mouvement
        transform.position = Vector3.Lerp(_precedentPassagePlateforme.position, _ciblePassagePlateforme.position, ecoulerPourcentage); // D�placement
        transform.rotation = Quaternion.Lerp(_precedentPassagePlateforme.rotation, _ciblePassagePlateforme.rotation, ecoulerPourcentage); // Rotation

        if (ecoulerPourcentage >= 1)
        {
            CibleProchainPassagePlateforme(); // D�finir la prochaine cible lorsque le d�placement est termin�
        }
    }
    // D�fini la prochaine cible de la plateforme
    private void CibleProchainPassagePlateforme()
    {
        _precedentPassagePlateforme = _passagePlateforme.ObtenirPassagePlateforme(_ciblePassagePlateformeIndex); // Plateforme pr�c�dente
        _ciblePassagePlateformeIndex = _passagePlateforme.ObtenirProchainePassagePlateformeIndex(_ciblePassagePlateformeIndex); // Index prochaine cible
        _ciblePassagePlateforme = _passagePlateforme.ObtenirPassagePlateforme(_ciblePassagePlateformeIndex); // Plateforme cible

        _tempsEcouler = 0;

        float distanceAPassagePlateforme = Vector3.Distance(_precedentPassagePlateforme.position, _ciblePassagePlateforme.position); // Calcul distance � parcourir
        _tempsAPassagePlateforme = distanceAPassagePlateforme / _vitesse;
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform parentTransforme = other.transform.parent; // Transform parent de l'objet entrant
        if (parentTransforme != null)
        {
            parentTransforme.SetParent(transform); // D�finition de la plateforme comme parent de l'objet
            suitObjets.Add(parentTransforme); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Transform parentTransform = other.transform.parent; // Transform parent de l'objet sortant
        if (parentTransform != null)
        {

                foreach (Transform obj in suitObjets) // Boucle � travers tous les objets suivis
                {
                    obj.SetParent(null); 
                }
                suitObjets.Clear(); // Effacer HashSet pour le nettoyage

        }
    }
}
