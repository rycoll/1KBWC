using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour {

    public GameObject playerHandDisplay;
    public GameObject opponentsDisplay;
    public GameObject opponentCardDisplay;

    public GameObject smallCardDisplayPrefab;
    public GameObject opponentPrefab;

    private void Start() {
        OpponentDisplay.UI = this;
    }

    public void AddCardToHandDisplay (Card card)
    {
        GameObject cardDisplay = Instantiate(smallCardDisplayPrefab) as GameObject;
        cardDisplay.transform.SetParent(playerHandDisplay.transform);

        CardDisplaySmall displayInfo = cardDisplay.GetComponent<CardDisplaySmall>();
        displayInfo.card = card;
    }

    public void RefreshOpponentDisplay (GamePlayer[] opponents) {
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
        foreach (Card card in opponent.Hand.GetCards()) {
            GameObject cardDisplay = Instantiate(smallCardDisplayPrefab) as GameObject;
            cardDisplay.transform.SetParent(opponentCardDisplay.transform);
            CardDisplaySmall displayInfo = cardDisplay.GetComponent<CardDisplaySmall>();
            displayInfo.card = card;
        }
    }
	
}
