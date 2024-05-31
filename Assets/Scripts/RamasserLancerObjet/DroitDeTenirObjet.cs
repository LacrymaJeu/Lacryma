using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Permet au joueur de ramasser des objets apr�s que l'objet a �t� ramass� et un texte apparait aussi pour 7 secondes

public class DroitDeTenirObjet : MonoBehaviour {
    public GameObject joueur;
    public GameObject[] objetsADesactiver; // Tableau d'objets � d�sactiver
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

        // Configurer l'att�nuation avec une distance maximale de 5 unit�s et une courbe lin�aire
        audioSourceBoucle.spatialBlend = 1.0f; // Son 3D
        audioSourceBoucle.maxDistance = 5.0f;
        audioSourceBoucle.rolloffMode = AudioRolloffMode.Linear;

        audioSourceBoucle.Play(); // Jouer le son en boucle d�s le d�part
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Player")) {
            joueur.GetComponent<JoueurRamasseLache>().enabled = true;
            // D�sactiver les objets du tableau
            DesactiverObjets();
            // Afficher le texte
            StartCoroutine(AfficherEtCacherTexte());
            cinematique.SetActive(true);

            // Jouer le son de ramassage
            audioSourceRamassage.Play();

            // Arr�ter le son en boucle
            audioSourceBoucle.Stop();
        }
    }

    void DesactiverObjets() {
        foreach (GameObject objet in objetsADesactiver) {
            if (objet != null) {
                // D�sactiver le Collider
                Collider colliderObjet = objet.GetComponent<Collider>();
                if (colliderObjet != null) {
                    colliderObjet.enabled = false;
                }
                // D�sactiver le Mesh Renderer
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
