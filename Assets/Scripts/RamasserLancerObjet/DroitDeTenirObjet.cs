using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Permet le joueur de ramasser des objets après que l'objet a été ramassé et un texte apparait aussi pour 6 secondes

public class DroitDeTenirObjet : MonoBehaviour
{
    public GameObject joueur;
    public GameObject[] objetsADesactiver; // Tableau d'objets à désactiver
    public TextMeshProUGUI texteTMP;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            joueur.GetComponent<JoueurRamasseLache>().enabled = true;
            // Désactive les objets du tableau
            DesactiverObjets();
            // Affiche le texte
            StartCoroutine(AfficherEtCacherTexte());
        }
    }

    void DesactiverObjets()
    {
        foreach (GameObject objet in objetsADesactiver)
        {
            if (objet != null)
            {
                // Désactive le Collider
                Collider colliderObjet = objet.GetComponent<Collider>();
                if (colliderObjet != null)
                {
                    colliderObjet.enabled = false;
                }
                // Désactive le Mesh Renderer
                Renderer rendererObjet = objet.GetComponent<Renderer>();
                if (rendererObjet != null)
                {
                    rendererObjet.enabled = false;
                }
            }
        }
    }

    IEnumerator AfficherEtCacherTexte()
    {
        texteTMP.gameObject.SetActive(true);
        yield return new WaitForSeconds(6f);
        texteTMP.gameObject.SetActive(false);
    }
}
