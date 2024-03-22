using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Trigger pour les dialogues

public class DeclencheurDialogue : MonoBehaviour {
    [SerializeField] private List<dialogueString> dialogueStrings = new List<dialogueString>(); // Liste des chaînes de dialogue
    [SerializeField] private Transform pourCameraNPC; // Référence à la caméra du NPC

    private bool aParler = false; // Variable pour vérifier si le dialogue a déjà commencé

    // Méthode appelée lorsqu'un autre collider entre en collision avec celui-ci

    public void Interact()
    {
        Debug.Log("Interact");

        if (!aParler)
        {
            // Start the dialogue directly without needing a collider trigger
            GestionDialogue gestionDialogue = FindFirstObjectByType<GestionDialogue>();
            if (gestionDialogue != null)
            {
                gestionDialogue.DialogueDebut(dialogueStrings, pourCameraNPC);
                aParler = true;
            }
        }
    }
}


// Classe représentant une chaîne de dialogue
[System.Serializable]
public class dialogueString {
    public string texte; // Texte que le NPC dit
    public bool laFin; // Indique si c'est la dernière ligne de la conversation

    [Header("Événements déclenchés")]
    public UnityEvent startDialogueEvent; // Événement déclenché au début de cette ligne de dialogue
    public UnityEvent endDialogueEvent; // Événement déclenché à la fin de cette ligne de dialogue
}

