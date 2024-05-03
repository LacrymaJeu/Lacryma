using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joue un son � des intervalles r�guliers d�finis par nous.

public class SonsRepetitionSec : MonoBehaviour
{
    public AudioSource sons;

    [SerializeField] private float intervalleEntreSons = 45f;

    void Start() {
        // D�marrage de la coroutine pour jouer le son 2 � intervalles r�guliers
        StartCoroutine(JouerSons());
    }

    IEnumerator JouerSons() {
        // Attendre pendant l'intervalle sp�cifi� avant de jouer le son 2
        yield return new WaitForSeconds(intervalleEntreSons);

        // Jouer le son 2
        sons.Play(); 

        // Ensuite, jouer le son 2 � intervalles r�guliers
        while (true) {
            yield return new WaitForSeconds(intervalleEntreSons);
            sons.Play();
        }
    }
}


