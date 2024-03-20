using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Trigger pour les dialogues

public class DeclencheurDialogue : MonoBehaviour {
    [SerializeField] private List<dialogueString> dialogueStrings = new List<dialogueString>(); // Liste des cha�nes de dialogue
    [SerializeField] private Transform pourCameraNPC; // R�f�rence � la cam�ra du NPC

    private bool aParler = false; // Variable pour v�rifier si le dialogue a d�j� commenc�

    // M�thode appel�e lorsqu'un autre collider entre en collision avec celui-ci
    private void OnTriggerEnter(Collider other) {
        // V�rification si l'autre objet est �tiquet� comme "Dialogue" et si le dialogue n'a pas encore commenc�
        if (other.CompareTag("Dialogue") && !aParler) {
            // D�but du dialogue en appelant la m�thode DialogueDebut() du script GestionDialogue attach� � l'autre objet
            other.gameObject.GetComponent<GestionDialogue>().DialogueDebut(dialogueStrings, pourCameraNPC);
            aParler = true; // Marquer que le dialogue a commenc�
        }
    }
}

// Classe repr�sentant une cha�ne de dialogue
[System.Serializable]
public class dialogueString {
    public string texte; // Texte que le NPC dit
    public bool laFin; // Indique si c'est la derni�re ligne de la conversation

    [Header("�v�nements d�clench�s")]
    public UnityEvent startDialogueEvent; // �v�nement d�clench� au d�but de cette ligne de dialogue
    public UnityEvent endDialogueEvent; // �v�nement d�clench� � la fin de cette ligne de dialogue
}