using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CardFinalisationUI : MonoBehaviour
{
    [SerializeField]
    private InputField nameInputField = null;
    [SerializeField]
    private Text rulesTextDisplay = null;
    [SerializeField]
    private Text validationErrorText = null;
    [SerializeField]
    private Image cardArtDisplay = null;

    [SerializeField]
    private EffectTabController effectTab = null;
    [SerializeField]
    private TagEditor tagTab = null;
    [SerializeField]
    private ArtEditor artTab = null;

    private BinaryCardLoader binaryCardLoader = new BinaryCardLoader();

    private void OnEnable() { 
        SetRulesText();
        SetCardArtDisplay();
    }

    private string GetCardName() {
        return nameInputField.text;
    }

    public void SetCardArtDisplay() {
        Texture2D texture = artTab.ExportTexture();
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, 300, 300), new Vector2(0.5f, 0.5f));
        cardArtDisplay.sprite = sprite;
    }

    public void SetRulesText() {
        string text = "";

        foreach(string tag in tagTab.GetTags()) {
            text += $"{tag}\n";
        }

        if (effectTab.GetNumberOfEffects() > 0) {
            List<byte> effectBytes = effectTab.GetConcatenatedEffects();
            RulesTextInterpreter RTI = new RulesTextInterpreter(effectBytes);
            text += RTI.GetFullRulesText();
        } else {
            text += "This card doesn't do anything yet. Add an effect in the Effects tab.";
        }

        rulesTextDisplay.text = text;
    }
 
    public void DisplayError(string errorMessage) {
        validationErrorText.text = errorMessage;
    }

    public void ValidateAndFinalise() {
        string cardName = GetCardName();
        if (string.IsNullOrEmpty(cardName)) {
            DisplayError("This card still needs a name.");
            return;
        }
        if (effectTab.GetNumberOfEffects() == 0) {
            DisplayError("This card needs at least one effect.");
            return;
        }
        if (artTab.ExportTexture() == null) {
            DisplayError("Something went wrong getting the art for your card :(");
            return;
        }
    
        CardData card = new CardData();
        card.Name = cardName;
        card.ArtTexture = artTab.ExportTexture();
        card.AddEffect(effectTab.GetConcatenatedEffects());
        foreach(string tag in tagTab.GetTags()) {
            card.AddTag(tag);
        }
        card.compilerVersion = EffectData.CompilerVersion;

        Debug.Log($"Saving a new card! {cardName}, {tagTab.GetTags().Count} tags, {effectTab.GetNumberOfEffects()} effects. [{card.GetID()}]");

        binaryCardLoader.SaveCards(new List<CardData>{ card });
        SceneManager.LoadScene("title");
    }
}
