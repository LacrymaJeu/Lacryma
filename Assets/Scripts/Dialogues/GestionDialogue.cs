using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

// Gestion des dialogues. G�re l'affichage des dialogues entre le joueur et les NPCS

public class GestionDialogue : MonoBehaviour
{
    [SerializeField] private GameObject dialogueParent; // Le conteneur des �l�ments de dialogue.
    [SerializeField] private TMP_Text dialogueText; // Le texte du dialogue.

    [SerializeField] private float vitesseEcriture = 0.05f; // Vitesse de l'�criture du texte quand le joueur le parle
    [SerializeField] private float vitesseTourne = 2f; // Vitesse de rotation de la cam�ra vers le NPC.

    private List<dialogueString> dialogueListe; // Liste des dialogues � afficher.
    private Transform cameraJoueur; // R�f�rence � la cam�ra du joueur.
    private int dialogueIndex = 0; // Index du dialogue en cours.
    public static bool enDialogue = false; // Indique si le joueur est en dialogue avec le NPC.
    public TextMeshProUGUI npcNomText; // Element UI pour afficher le nom du NPC


    private void Start()
    {
        dialogueParent.SetActive(false); // D�sactive le conteneur de dialogue au d�marrage.
        cameraJoueur = Camera.main.transform; // Initialise la r�f�rence � la cam�ra du joueur.
    }

    // D�marre le dialogue avec les textes sp�cifi�s et le NPC donn� en param�tre.
    public void DialogueDebut(List<dialogueString> textDevoirAfficher, Transform NPC)
    {
        if (enDialogue) return; // Si le joueur est d�j� en dialogue, ne pas d�marrer un nouveau dialogue.

        enDialogue = true; // Met � jour l'�tat du dialogue.
        dialogueParent.SetActive(true);
        Player.peutBougerDialogue = false; // Emp�che le joueur de bouger pendant le dialogue.

        Cursor.lockState = CursorLockMode.None; // D�verrouille le curseur.
        Cursor.visible = true; // Rend le curseur visible.

        StartCoroutine(TourneCameraVersNPC(NPC));

        dialogueListe = textDevoirAfficher;
        dialogueIndex = 0;

        StartCoroutine(AfficheDialogue());
    }

    // Coroutine pour tourner la cam�ra vers le NPC.
    private IEnumerator TourneCameraVersNPC(Transform NPC)
    {
        Quaternion commenceRotation = cameraJoueur.rotation; // Rotation initiale de la cam�ra.
        Quaternion cibleRotation = Quaternion.LookRotation(NPC.position - cameraJoueur.position); // Rotation vers le NPC.

        float tempsEcouler = 0f;
        while (tempsEcouler < 1f)
        {
            cameraJoueur.rotation = Quaternion.Slerp(commenceRotation, cibleRotation, tempsEcouler);
            tempsEcouler += Time.deltaTime * vitesseTourne;
            yield return null;
        }

        cameraJoueur.rotation = cibleRotation;
    }


    // Coroutine pour afficher les dialogues un par un.
    private IEnumerator AfficheDialogue()
    {
        while (dialogueIndex < dialogueListe.Count)
        {
            dialogueString ligne = dialogueListe[dialogueIndex]; // R�cup�re la ligne de dialogue.

            ligne.startDialogueEvent?.Invoke(); // D�clenche l'�v�nement de d�but de dialogue.

            yield return StartCoroutine(EcritTexte(ligne.texte)); // D�marre la coroutine d'�criture du texte.

            ligne.endDialogueEvent?.Invoke(); // D�clenche l'�v�nement de fin de dialogue.

            if (ligne.laFin) // V�rifie si c'est la fin du dialogue.
            {
                DialogueArrete(); // Arr�te le dialogue si c'est la fin.
                yield break; // Sort de la coroutine.
            }

            dialogueIndex++; // Passe au dialogue suivant.
        }

        DialogueArrete(); // Arr�te le dialogue une fois tous les dialogues affich�s.
    }

    // Coroutine pour �crire le texte progressivement.
    private IEnumerator EcritTexte(string text)
    {
        dialogueText.text = ""; // R�initialise le texte du dialogue.
        foreach (char lettre in text.ToCharArray()) // Parcourt chaque lettre du texte.
        {
            dialogueText.text += lettre; // Ajoute la lettre au texte du dialogue.
            yield return new WaitForSeconds(vitesseEcriture); // Attend un court instant avant la prochaine lettre.
        }

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0)); // Attend que le joueur appuie sur le bouton de la souris.

        // dialogueIndex++; // Passe au dialogue suivant.
    }

    // Arr�te le dialogue et r�initialise les param�tres.
    private void DialogueArrete()
    {
        enDialogue = false; // Met � jour l'�tat du dialogue.

        StopAllCoroutines(); // Arr�te toutes les coroutines en cours.
        dialogueText.text = ""; // Efface le texte du dialogue.
        dialogueParent.SetActive(false); // D�sactive le conteneur de dialogue.

        Player.peutBougerDialogue = true; // Autorise � nouveau le mouvement du joueur.

        Cursor.lockState = CursorLockMode.Locked; // Verrouille le curseur.
        Cursor.visible = false; // Cache le curseur.

        ResetEtatDialogue();
    }
    // Permet de mettre le nom du NPC
    public void MetNpcNom(string name)
    {
        if (npcNomText != null)
            npcNomText.text = name;
    }

    // R�initialise l'�tat de dialogue.
    private void ResetEtatDialogue()
    {
        dialogueIndex = 0;
        dialogueListe = null;
    }
}
