using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

// Gestion des dialogues. Gère l'affichage des dialogues entre le joueur et les NPCS

public class GestionDialogue : MonoBehaviour
{
    [SerializeField] private GameObject dialogueParent; // Le conteneur des éléments de dialogue.
    [SerializeField] private TMP_Text dialogueText; // Le texte du dialogue.

    [SerializeField] private float vitesseEcriture = 0.05f; // Vitesse de l'écriture du texte quand le joueur le parle
    [SerializeField] private float vitesseTourne = 2f; // Vitesse de rotation de la caméra vers le NPC.

    private List<dialogueString> dialogueListe; // Liste des dialogues à afficher.
    private Transform cameraJoueur; // Référence à la caméra du joueur.
    private int dialogueIndex = 0; // Index du dialogue en cours.
    public static bool enDialogue = false; // Indique si le joueur est en dialogue avec le NPC.
    public TextMeshProUGUI npcNomText; // Element UI pour afficher le nom du NPC


    private void Start()
    {
        dialogueParent.SetActive(false); // Désactive le conteneur de dialogue au démarrage.
        cameraJoueur = Camera.main.transform; // Initialise la référence à la caméra du joueur.
    }

    // Démarre le dialogue avec les textes spécifiés et le NPC donné en paramètre.
    public void DialogueDebut(List<dialogueString> textDevoirAfficher, Transform NPC)
    {
        if (enDialogue) return; // Si le joueur est déjà en dialogue, ne pas démarrer un nouveau dialogue.

        enDialogue = true; // Met à jour l'état du dialogue.
        dialogueParent.SetActive(true);
        Player.peutBougerDialogue = false; // Empêche le joueur de bouger pendant le dialogue.

        Cursor.lockState = CursorLockMode.None; // Déverrouille le curseur.
        Cursor.visible = true; // Rend le curseur visible.

        StartCoroutine(TourneCameraVersNPC(NPC));

        dialogueListe = textDevoirAfficher;
        dialogueIndex = 0;

        StartCoroutine(AfficheDialogue());
    }

    // Coroutine pour tourner la caméra vers le NPC.
    private IEnumerator TourneCameraVersNPC(Transform NPC)
    {
        Quaternion commenceRotation = cameraJoueur.rotation; // Rotation initiale de la caméra.
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
            dialogueString ligne = dialogueListe[dialogueIndex]; // Récupère la ligne de dialogue.

            ligne.startDialogueEvent?.Invoke(); // Déclenche l'événement de début de dialogue.

            yield return StartCoroutine(EcritTexte(ligne.texte)); // Démarre la coroutine d'écriture du texte.

            ligne.endDialogueEvent?.Invoke(); // Déclenche l'événement de fin de dialogue.

            if (ligne.laFin) // Vérifie si c'est la fin du dialogue.
            {
                DialogueArrete(); // Arrête le dialogue si c'est la fin.
                yield break; // Sort de la coroutine.
            }

            dialogueIndex++; // Passe au dialogue suivant.
        }

        DialogueArrete(); // Arrête le dialogue une fois tous les dialogues affichés.
    }

    // Coroutine pour écrire le texte progressivement.
    private IEnumerator EcritTexte(string text)
    {
        dialogueText.text = ""; // Réinitialise le texte du dialogue.
        foreach (char lettre in text.ToCharArray()) // Parcourt chaque lettre du texte.
        {
            dialogueText.text += lettre; // Ajoute la lettre au texte du dialogue.
            yield return new WaitForSeconds(vitesseEcriture); // Attend un court instant avant la prochaine lettre.
        }

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0)); // Attend que le joueur appuie sur le bouton de la souris.

        // dialogueIndex++; // Passe au dialogue suivant.
    }

    // Arrête le dialogue et réinitialise les paramètres.
    private void DialogueArrete()
    {
        enDialogue = false; // Met à jour l'état du dialogue.

        StopAllCoroutines(); // Arrête toutes les coroutines en cours.
        dialogueText.text = ""; // Efface le texte du dialogue.
        dialogueParent.SetActive(false); // Désactive le conteneur de dialogue.

        Player.peutBougerDialogue = true; // Autorise à nouveau le mouvement du joueur.

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

    // Réinitialise l'état de dialogue.
    private void ResetEtatDialogue()
    {
        dialogueIndex = 0;
        dialogueListe = null;
    }
}
