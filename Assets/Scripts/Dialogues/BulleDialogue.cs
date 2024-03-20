using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulleDialogue : MonoBehaviour
{
    private SpriteRenderer backgroundSpriteRenderer;
    private TextMeshPro textMeshPro;

   // public static void Create(Transform parent, Vector3 localPosition, string text) {
    //    Instantiate()
   // }

    private void Awake() {
        // Trouver les composants SpriteRenderer et TextMeshPro à partir des enfants de cet objet
        backgroundSpriteRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
    }

    private void Start() {
        // Appeler Setup() avec le texte initial
        Setup("Hi my guy!");
    }

    private void Setup(string text) {
        // Définir le texte dans TextMeshPro
        textMeshPro.SetText(text);
        // Mettre à jour le maillage du texte
        textMeshPro.ForceMeshUpdate();
        // Obtenir la taille rendue du texte
        Vector2 textSize = textMeshPro.GetRenderedValues(false);

        // Ajouter un rembourrage au texte pour éviter que le texte ne touche les bords du fond
        Vector2 padding = new Vector2(0f, 0f);

        // Définir la taille du sprite de fond en fonction de la taille du texte et du rembourrage
        backgroundSpriteRenderer.size = textSize + padding;
    }
}
