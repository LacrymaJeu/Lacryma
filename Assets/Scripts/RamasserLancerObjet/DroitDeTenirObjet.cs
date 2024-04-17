using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Permet le joueur de ramasser des objets apr�s que l'objet a �t� ramasser et un texte appara�t aussi pour 3 secondes

public class DroitDeTenirObjet : MonoBehaviour
{
    public GameObject joueur; // Script � activer sur le joueur
    public GameObject objetADesactiver; // Objet � d�sactiver
    public TextMeshProUGUI texteTMP; // TextMeshProUGUI � afficher

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            joueur.GetComponent<JoueurRamasseLache>().enabled = true;
            Debug.Log("Le script sur le joueur a �t� activ�.");

            // D�sactive le Collider et le Mesh Renderer de l'objet
            DesactiverObjet();

            // Affiche le texte
            StartCoroutine(AfficherEtCacherTexte());
        }
    }

    void DesactiverObjet()
    {
        // D�sactive le Collider
        Collider colliderObjet = objetADesactiver.GetComponent<Collider>();
        if (colliderObjet != null)
        {
            colliderObjet.enabled = false;
            Debug.Log("Le collider de l'objet a �t� d�sactiv�.");
        }
        else
        {
            Debug.LogWarning("Collider non trouv� sur l'objet.");
        }

        // D�sactive le Mesh Renderer
        Renderer rendererObjet = objetADesactiver.GetComponent<Renderer>();
        if (rendererObjet != null)
        {
            rendererObjet.enabled = false;
            Debug.Log("Le Mesh Renderer de l'objet a �t� d�sactiv�.");
        }
        else
        {
            Debug.LogWarning("Renderer non trouv� sur l'objet.");
        }
    }

    IEnumerator AfficherEtCacherTexte()
    {
        // Active le texte
        texteTMP.gameObject.SetActive(true);

        // Attend 3 secondes
        yield return new WaitForSeconds(3f);

        // D�sactive le texte
        texteTMP.gameObject.SetActive(false);
    }
}
