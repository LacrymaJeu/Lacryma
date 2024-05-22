using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet le joueur de ramasser des objets dans la sc�ne
public class JoueurRamasseLache : MonoBehaviour {
    // R�f�rence � la transform de la cam�ra du joueur
    [SerializeField] private Transform joueurCameraTransform;
    // R�f�rence � la transform de l'objet � une distance permettant de le ramasser
    [SerializeField] private Transform objetDistancePourRamasser;
    // Masque utilis� pour filtrer les objets pouvant �tre ramass�s
    [SerializeField] private LayerMask masquePourRamasser;

    // R�f�rence � l'objet actuellement tenu par le joueur
    private ObjetTenable objetTenable;

    // R�f�rences aux clips audio pour les sons de ramassage et de l�cher
    [SerializeField] private AudioClip sonRamassage;
    [SerializeField] private AudioClip sonLacher;

    // Composant AudioSource pour jouer les sons
    private AudioSource audioSource;

    private void Start() {
        // Obtient le composant AudioSource attach� � ce GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) {
            // Ajoute un AudioSource si aucun n'est trouv�
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update() {
        // V�rifie si la touche E est pes�e
        if (Input.GetKeyDown(KeyCode.E)) {
            // Si aucun objet n'est tenu
            if (objetTenable == null) {
                // Distance maximale � laquelle le joueur peut ramasser un objet
                float ramasseDistance = 12f;
                // Lance un rayon depuis la position de la cam�ra du joueur vers l'avant
                if (Physics.Raycast(joueurCameraTransform.position, joueurCameraTransform.forward, out RaycastHit raycastFrapper, ramasseDistance, masquePourRamasser)) {
                    Debug.Log(raycastFrapper.transform);
                    // V�rifie si l'objet touch� poss�de le composant ObjetTenable
                    if (raycastFrapper.transform.TryGetComponent(out objetTenable)) {
                        // Fait appel � la fonction Prendre de l'autre script ObjetTenable pour le ramasser
                        objetTenable.Prendre(objetDistancePourRamasser);

                        // Joue le son de ramassage
                        StartCoroutine(JouerSonPourDuree(sonRamassage, 1f));
                    }
                }
            } else {
                // Si un objet est d�j� tenu, le joueur le l�che
                objetTenable.Lacher();

                // Joue le son de l�cher
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
