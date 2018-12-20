using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    public GameObject playerHandDisplay;
    public GameObject opponentsDisplay;

    public GameObject smallCardDisplayPrefab;
    public GameObject opponentPrefab;

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
            display.RefreshDisplay(opponent.Name, opponent.Hand.ToString(), opponent.Points.ToString());

            opponentDisplayObj.transform.SetParent(opponentsDisplay.transform);
        }
    }
	
}
