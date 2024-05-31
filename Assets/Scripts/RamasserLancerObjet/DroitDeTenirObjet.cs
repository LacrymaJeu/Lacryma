using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Permet au joueur de ramasser des objets après que l'objet a été ramassé et un texte apparait aussi pour 7 secondes

public class DroitDeTenirObjet : MonoBehaviour {
    public GameObject joueur;
    public GameObject[] objetsADesactiver; // Tableau d'objets à désactiver
    public TextMeshProUGUI texteTMP;
    public GameObject cinematique;

    // Ajouter une variable pour le son de ramassage
    public AudioClip sonDeRamassage;
    private AudioSource audioSourceRamassage;

    // Ajouter une variable pour le son en boucle
    public AudioClip sonEnBoucle;
    private AudioSource audioSourceBoucle;

    private void Start() {
        // Initialiser l'AudioSource pour le son de ramassage
        audioSourceRamassage = gameObject.AddComponent<AudioSource>();
        audioSourceRamassage.clip = sonDeRamassage;

        // Initialiser l'AudioSource pour le son en boucle
        audioSourceBoucle = gameObject.AddComponent<AudioSource>();
        audioSourceBoucle.clip = sonEnBoucle;
        audioSourceBoucle.loop = true;

        // Configurer l'atténuation avec une distance maximale de 5 unités et une courbe linéaire
        audioSourceBoucle.spatialBlend = 1.0f; // Son 3D
        audioSourceBoucle.maxDistance = 5.0f;
        audioSourceBoucle.rolloffMode = AudioRolloffMode.Linear;

        audioSourceBoucle.Play(); // Jouer le son en boucle dès le départ
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Player")) {
            joueur.GetComponent<JoueurRamasseLache>().enabled = true;
            // Désactiver les objets du tableau
            DesactiverObjets();
            // Afficher le texte
            StartCoroutine(AfficherEtCacherTexte());
            cinematique.SetActive(true);

            // Jouer le son de ramassage
            audioSourceRamassage.Play();

            // Arrêter le son en boucle
            audioSourceBoucle.Stop();
        }
    }

    void DesactiverObjets() {
        foreach (GameObject objet in objetsADesactiver) {
            if (objet != null) {
                // Désactiver le Collider
                Collider colliderObjet = objet.GetComponent<Collider>();
                if (colliderObjet != null) {
                    colliderObjet.enabled = false;
                }
                // Désactiver le Mesh Renderer
                Renderer rendererObjet = objet.GetComponent<Renderer>();
                if (rendererObjet != null) {
                    rendererObjet.enabled = false;
                }
            }
        }
    }

    IEnumerator AfficherEtCacherTexte() {
        texteTMP.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        texteTMP.gameObject.SetActive(false);
    }
}
