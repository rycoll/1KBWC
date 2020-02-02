using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardDisplaySmall : MonoBehaviour, IPointerClickHandler {

    // make sure this is assigned as an INSTANCE of the card type
    // two cards of the same type shouldn't both be bound here
    public Card card;
    public Text titleText;
    public Image cardImage;

    public GameObject bigDisplayPrefab;

	void Start () {
        RefreshDisplay();
	}

    public void SetCard (Card c)
    {
        this.card = c;
        RefreshDisplay();
    }

    private void RefreshDisplay ()
    {
        titleText.text = card.GetName();
        cardImage.sprite = (Sprite) card.cardArt;
    }

    // on mouse-over, display large version of card
    public void OnPointerClick(PointerEventData eventData)
    {   
        if (eventData.button == PointerEventData.InputButton.Right) {
            GameObject bigDisplay = Instantiate(bigDisplayPrefab) as GameObject;
            
            bigDisplay.transform.position = this.transform.position;

            // have to do some weird stuff about parenting for proper behaviour
            ReparentChildCardUI reparentInfo = this.transform.parent.GetComponent<ReparentChildCardUI>();
            if (reparentInfo) {
                bigDisplay.transform.SetParent(reparentInfo.targetContainer);
                if (reparentInfo.hand) {
                    bigDisplay.transform.Translate(0f, 50f, 0f);
                }
            } else {
                Debug.Log("Couldn't find the appropriate UI parent!");
                bigDisplay.transform.SetParent(this.transform);
            }
            bigDisplay.GetComponent<CardDisplayLarge>().SetCard(this.card);
        }
    }

}
