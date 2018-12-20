using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    public GameObject playerHandDisplay;

    public GameObject smallCardDisplayPrefab;

    public void AddCardToHandDisplay (Card card)
    {
        // this is broken - cards are instatiated but don't go to the right place
        GameObject cardDisplay = Instantiate(smallCardDisplayPrefab) as GameObject;
        cardDisplay.transform.SetParent(playerHandDisplay.transform);

        CardDisplaySmall displayInfo = cardDisplay.GetComponent<CardDisplaySmall>();
        displayInfo.card = card;
    }
	
}
