using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour {

    public GameObject playerHandDisplay;
    public Text playerPointText;
    public GameObject opponentsDisplay;
    public GameObject opponentCardDisplay;
    public Text deckText;
    public Text discardText;
    public Text flagsText;
    public GameObject discardedCardsDisplay;
    public GameObject choiceDisplay;

    public GameObject cardInHandDisplayPrefab;
    public GameObject smallCardDisplayPrefab;
    public GameObject opponentPrefab;
    public GameObject choiceItemPrefab;

    public void Start() {
        OpponentDisplay.UI = this;
    }        
    public void AddCardToHandDisplay (Card card)
    {
        GameObject cardDisplay = Instantiate(cardInHandDisplayPrefab) as GameObject;
        cardDisplay.transform.SetParent(playerHandDisplay.transform);

        CardDisplaySmall displayInfo = cardDisplay.GetComponent<CardDisplaySmall>();
        displayInfo.card = card;
    }

    public void SetDeckText (int n) {
        deckText.text = "DECK\n" + n.ToString();
    }

    public void UpdatePointDisplays (GamePlayer local, GamePlayer[] opponents) {
        playerPointText.text = local.Points.ToString() + " pts";
        RefreshOpponentDisplay(opponents);
    }

    public void RefreshOpponentDisplay (GamePlayer[] opponents) {
        foreach (Transform child in opponentsDisplay.transform) {
            GameObject.Destroy(child.gameObject);
        }
        foreach (GamePlayer opponent in opponents) {
            GameObject opponentDisplayObj = Instantiate(opponentPrefab) as GameObject;
            OpponentDisplay display = opponentDisplayObj.GetComponent<OpponentDisplay>();
            
            display.SetOpponent(opponent);
            display.RefreshDisplay();
            opponentDisplayObj.transform.SetParent(opponentsDisplay.transform);
        }
    }

    public void DisplayOpponentCards (GamePlayer opponent) {
        foreach (Transform child in opponentCardDisplay.transform) {
            GameObject.Destroy(child.gameObject);
        }

        Image background = opponentCardDisplay.GetComponent<Image>();
        background.color = opponent.Colour;

        Card[] opponentCards = GetComponent<GameController>().Table.GetCardsByPlayer(opponent);
        foreach (Card card in opponentCards) {
            GameObject cardDisplay = Instantiate(smallCardDisplayPrefab) as GameObject;
            cardDisplay.transform.SetParent(opponentCardDisplay.transform);
            CardDisplaySmall displayInfo = cardDisplay.GetComponent<CardDisplaySmall>();
            displayInfo.card = card;
        }
    }

    public void SetDiscardText (int n) {
        discardText.text = "DISCARD\n" + n.ToString();
    }

    public void AddToDiscardDisplay (Card card) {
        GameObject cardDisplay = Instantiate(smallCardDisplayPrefab) as GameObject;
        cardDisplay.transform.SetParent(discardedCardsDisplay.transform);

        CardDisplaySmall displayInfo = cardDisplay.GetComponent<CardDisplaySmall>();
        displayInfo.card = card;
    }

    public void ToggleDisplayDiscard () {
        GameObject parent = discardedCardsDisplay.transform.parent.gameObject;
        parent.SetActive(!parent.activeSelf);
    }

    public void UpdateFlagsText () {
        GameVariables gameVariables = GetComponent<GameController>().Variables;
        string newText = "";

        foreach (string str in gameVariables.GetFlags()) {
            newText += str + "\n";
        }
        foreach (KeyValuePair<string, string> kvp in gameVariables.GetVariables()) {
            newText += kvp.Key + ": " + kvp.Value + "\n";
        }
        foreach (KeyValuePair<string, int> kvp in gameVariables.GetCounters()) {
            newText += kvp.Key + ": " + kvp.Value + "\n";
        }

        flagsText.text = newText;
    }

    public void PresentChoiceOfPlayers(List<GamePlayer> players, ChoiceCallback callback) {
        choiceDisplay.transform.parent.gameObject.SetActive(true);
        foreach (GamePlayer player in players) {
            GameObject display = Instantiate(choiceItemPrefab) as GameObject;
            display.transform.SetParent(choiceDisplay.transform);
            
            ChoiceItemDisplay displayInfo = display.GetComponent<ChoiceItemDisplay>();
            displayInfo.SetText(
                player.Name,
                player.Points + "pts\n" + player.Hand.GetNumCards() + "cards in hand"
            );
            Button button = display.GetComponent<Button>();
            button.onClick.AddListener(() => callback((object) player, GetComponent<GameController>()));
        }
    }

    public void PresentChoiceOfCards(List<Card> cards, ChoiceCallback callback) {
        choiceDisplay.transform.parent.gameObject.SetActive(true);
        foreach (Card card in cards) {
            GameObject display = Instantiate(choiceItemPrefab) as GameObject;
            display.transform.SetParent(choiceDisplay.transform);
            
            ChoiceItemDisplay displayInfo = display.GetComponent<ChoiceItemDisplay>();
            displayInfo.SetText(
                card.cardName,
                card.GetRuleText()
            );
            Button button = display.GetComponent<Button>();
            button.onClick.AddListener(() => callback((object) card, GetComponent<GameController>()));
        }
    }

    public void RemoveChoiceDisplay () {
        foreach (Transform child in choiceDisplay.transform) {
            GameObject.Destroy(child.gameObject);
        }
        choiceDisplay.transform.parent.gameObject.SetActive(false);
    }
	
}
