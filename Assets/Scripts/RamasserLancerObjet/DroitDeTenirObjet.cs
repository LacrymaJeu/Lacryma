using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Permet le joueur de ramasser des objets après que l'objet a été ramasser et un texte apparaît aussi pour 3 secondes

public class DroitDeTenirObjet : MonoBehaviour
{
    public GameObject joueur; // Script à activer sur le joueur
    public GameObject objetADesactiver; // Objet à désactiver
    public TextMeshProUGUI texteTMP; // TextMeshProUGUI à afficher

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            joueur.GetComponent<JoueurRamasseLache>().enabled = true;
            Debug.Log("Le script sur le joueur a été activé.");

            // Désactive le Collider et le Mesh Renderer de l'objet
            DesactiverObjet();

            // Affiche le texte
            StartCoroutine(AfficherEtCacherTexte());
        }
    }

    void DesactiverObjet()
    {
        // Désactive le Collider
        Collider colliderObjet = objetADesactiver.GetComponent<Collider>();
        if (colliderObjet != null)
        {
            colliderObjet.enabled = false;
            Debug.Log("Le collider de l'objet a été désactivé.");
        }
        else
        {
            Debug.LogWarning("Collider non trouvé sur l'objet.");
        }

        // Désactive le Mesh Renderer
        Renderer rendererObjet = objetADesactiver.GetComponent<Renderer>();
        if (rendererObjet != null)
        {
            rendererObjet.enabled = false;
            Debug.Log("Le Mesh Renderer de l'objet a été désactivé.");
        }
        else
        {
            Debug.LogWarning("Renderer non trouvé sur l'objet.");
        }
    }

    IEnumerator AfficherEtCacherTexte()
    {
        // Active le texte
        texteTMP.gameObject.SetActive(true);

        // Attend 3 secondes
        yield return new WaitForSeconds(3f);

        // Désactive le texte
        texteTMP.gameObject.SetActive(false);
    }
}
