using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Trigger pour les dialogues

public class DeclencheurDialogue : MonoBehaviour
{
    [SerializeField] private List<dialogueString> dialogueStrings = new List<dialogueString>();
    [SerializeField] private Transform pourCameraNPC;

    private bool aParler = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dialogue") && !aParler)
        {
            other.gameObject.GetComponent<GestionDialogue>().DialogueDebut(dialogueStrings, pourCameraNPC);
            aParler = true;
        }
    }
}

[System.Serializable]

public class dialogueString
{
    public string texte; // Text le NPC dit
    public bool laFin; // Dernière ligne de la convo

    [Header("Événements déclenchés")]
    // Il faut les mettre en Anglais
    public UnityEvent startDialogueEvent;
    public UnityEvent endDialogueEvent;
}
