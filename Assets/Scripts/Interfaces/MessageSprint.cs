using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Affiche le message qui dis au joueur qu'il peut sprinter après qu'il rentre dans le trigger

public class MessageSprint : MonoBehaviour
{
    public string messageSprint = "Message à afficher";
    public float displayTemps = 6f;
    public TextMeshProUGUI messageText;
    public GameObject playerObjet;

    private bool aTrigger = false;

    // Quand le joueur rentre dans le trigger
    private void OnTriggerEnter(Collider other)
    {
        if (!aTrigger && other.gameObject == playerObjet)
        {
            aTrigger = true;
            StartCoroutine(MontreMessage());
        }
    }

    // Affiche le message de sprint
    private IEnumerator MontreMessage()
    {
        messageText.text = messageSprint;
        messageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(displayTemps);
        messageText.gameObject.SetActive(false);
    }
}
