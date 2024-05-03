using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joue un son à des intervalles réguliers définis par nous.

public class SonsRepetitionSec : MonoBehaviour
{
    public AudioSource sons;

    [SerializeField] private float intervalleEntreSons = 45f;

    void Start() {
        // Démarrage de la coroutine pour jouer le son 2 à intervalles réguliers
        StartCoroutine(JouerSons());
    }

    IEnumerator JouerSons() {
        // Attendre pendant l'intervalle spécifié avant de jouer le son 2
        yield return new WaitForSeconds(intervalleEntreSons);

        // Jouer le son 2
        sons.Play(); 

        // Ensuite, jouer le son 2 à intervalles réguliers
        while (true) {
            yield return new WaitForSeconds(intervalleEntreSons);
            sons.Play();
        }
    }
}


