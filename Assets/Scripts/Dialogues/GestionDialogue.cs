using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

// Gestion des dialogues.

public class GestionDialogue : MonoBehaviour
{
    [SerializeField] private GameObject dialogueParent;
    [SerializeField] private TMP_Text dialogueText;

    [SerializeField] private float vitesseEcriture = 0.05f;
    [SerializeField] private float vitesseTourne = 2f;

    private List<dialogueString> dialogueListe;

    [Header("Joueur")]
    private Transform cameraJoueur;

    private int dialogueIndex = 0;

    private void Start()
    {
        dialogueParent.SetActive(false);
        cameraJoueur = Camera.main.transform;

    }

    public void DialogueDebut(List<dialogueString> textDevoirAfficher, Transform NPC)
    {
        dialogueParent.SetActive(true);
        player.peutBouger = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        StartCoroutine(TourneCameraVersNPC(NPC));

        dialogueListe = textDevoirAfficher;
        dialogueIndex = 0;

        StartCoroutine(AfficheDialogue());
    }

    private IEnumerator TourneCameraVersNPC(Transform NPC)
    {
        Quaternion commenceRotation = cameraJoueur.rotation;
        Quaternion cibleRotation = Quaternion.LookRotation(NPC.position - cameraJoueur.position);

        float tempsEcouler = 0f;
        while (tempsEcouler < 1f)
        {
            cameraJoueur.rotation = Quaternion.Slerp(commenceRotation, cibleRotation, tempsEcouler);
            tempsEcouler += Time.deltaTime * vitesseTourne;
            yield return null;
        }

        cameraJoueur.rotation = cibleRotation;
    }

    private IEnumerator AfficheDialogue()
    {
        while (dialogueIndex < dialogueListe.Count)
        {
            dialogueString ligne = dialogueListe[dialogueIndex];

            ligne.startDialogueEvent?.Invoke();

            yield return StartCoroutine(EcritTexte(ligne.texte));

            ligne.endDialogueEvent?.Invoke();

        }

        DialogueArrete();
    }

    private IEnumerator EcritTexte(string text)
    {
        dialogueText.text = "";
        foreach (char lettre in text.ToCharArray())
        {
            dialogueText.text += lettre;
            yield return new WaitForSeconds(vitesseEcriture);
        }
        
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        if (dialogueListe[dialogueIndex].laFin)
        {
            DialogueArrete();
        }

        dialogueIndex++;
    }

    private void DialogueArrete()
    {
        StopAllCoroutines();
        dialogueText.text = "";
        dialogueParent.SetActive(false);

        player.peutBouger = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
