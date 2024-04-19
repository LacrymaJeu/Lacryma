using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

// Trigger pour les dialogues

public class DeclencheurDialogue : MonoBehaviour
{
    [SerializeField] private List<dialogueString> dialogueStrings = new List<dialogueString>(); // Liste des cha�nes de dialogue
    [SerializeField] private Transform pourCameraNPC; // R�f�rence � la cam�ra du NPC
    [SerializeField] private string npcNom; // Nom du NPC pour ce dialogue

    // M�thode appel�e lorsqu'un autre collider entre en collision avec celui-ci
    public void Interact()
    {
        Debug.Log("Interact");

        if (GestionDialogue.enDialogue) return; // Si le joueur est d�j� en dialogue, ne pas d�marrer un nouveau dialogue.

        // Start the dialogue directly without needing a collider trigger
        GestionDialogue gestionDialogue = FindFirstObjectByType<GestionDialogue>();
        if (gestionDialogue != null)
        {
            gestionDialogue.MetNpcNom(npcNom); // D�finit le nom du NPC dans GestionDialogue
            gestionDialogue.DialogueDebut(dialogueStrings, pourCameraNPC);
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

