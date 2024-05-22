using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet le joueur de ramasser des objets dans la scène
public class JoueurRamasseLache : MonoBehaviour {
    // Référence à la transform de la caméra du joueur
    [SerializeField] private Transform joueurCameraTransform;
    // Référence à la transform de l'objet à une distance permettant de le ramasser
    [SerializeField] private Transform objetDistancePourRamasser;
    // Masque utilisé pour filtrer les objets pouvant être ramassés
    [SerializeField] private LayerMask masquePourRamasser;

    // Référence à l'objet actuellement tenu par le joueur
    private ObjetTenable objetTenable;

    // Références aux clips audio pour les sons de ramassage et de lâcher
    [SerializeField] private AudioClip sonRamassage;
    [SerializeField] private AudioClip sonLacher;

    // Composant AudioSource pour jouer les sons
    private AudioSource audioSource;

    private void Start() {
        // Obtient le composant AudioSource attaché à ce GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) {
            // Ajoute un AudioSource si aucun n'est trouvé
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update() {
        // Vérifie si la touche E est pesée
        if (Input.GetKeyDown(KeyCode.E)) {
            // Si aucun objet n'est tenu
            if (objetTenable == null) {
                // Distance maximale à laquelle le joueur peut ramasser un objet
                float ramasseDistance = 12f;
                // Lance un rayon depuis la position de la caméra du joueur vers l'avant
                if (Physics.Raycast(joueurCameraTransform.position, joueurCameraTransform.forward, out RaycastHit raycastFrapper, ramasseDistance, masquePourRamasser)) {
                    Debug.Log(raycastFrapper.transform);
                    // Vérifie si l'objet touché possède le composant ObjetTenable
                    if (raycastFrapper.transform.TryGetComponent(out objetTenable)) {
                        // Fait appel à la fonction Prendre de l'autre script ObjetTenable pour le ramasser
                        objetTenable.Prendre(objetDistancePourRamasser);

                        // Joue le son de ramassage
                        StartCoroutine(JouerSonPourDuree(sonRamassage, 1f));
                    }
                }
            } else {
                // Si un objet est déjà tenu, le joueur le lâche
                objetTenable.Lacher();

                // Joue le son de lâcher
                StartCoroutine(JouerSonPourDuree(sonLacher, 1f));

                objetTenable = null;
            }
        }
    }

    private IEnumerator JouerSonPourDuree(AudioClip clip, float duree) {
        audioSource.PlayOneShot(clip);
        yield return new WaitForSeconds(duree);
        audioSource.Stop();
    }
}
