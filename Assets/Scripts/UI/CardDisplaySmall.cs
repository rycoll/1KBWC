using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardDisplaySmall : MonoBehaviour, IPointerEnterHandler {

    // make sure this is assigned as an INSTANCE of the card type
    // two cards of the same type shouldn't both be bound here
    public Card card;
    public Text titleText;
    public Image cardImage;

    public GameObject bigDisplayPrefab;

	void Start () {
        titleText.text = card.cardName;
        cardImage.sprite = card.cardArt;
	}

    // on mouse-over, display large version of card
    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject bigDisplay = Instantiate(bigDisplayPrefab) as GameObject;
        // this is gross, but basically we want to attach it to the canvas
        bigDisplay.transform.SetParent(this.transform);
        bigDisplay.transform.position = this.transform.position;
        if (card.cardLocation == Card.Location.HAND)
        {
            bigDisplay.transform.Translate(0, 50f, 0);
        }

        bigDisplay.GetComponent<CardDisplayLarge>().SetCard(this.card);
    }
    

}
