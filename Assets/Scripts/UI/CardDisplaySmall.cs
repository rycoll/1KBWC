using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardDisplaySmall : MonoBehaviour, IPointerClickHandler {

    public Card card;
    public Text titleText;
    public Image cardImage;
    [SerializeField] 
    private Image background;
    private bool selected = false;
    private bool selectable = false;

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
        cardImage.sprite = card.GetSprite();
    }

    public void SetBackground (bool active, Color color) {
        background.gameObject.SetActive(active);
        background.color = color;
    }

    public void SetSelected (bool b) {
        selected = b;
        SetBackground(b, new Color(255f, 255f, 0));
    }

    public void SetSelectable (bool b) {
        selectable = b;
    }

    public bool isSelected () {
        return selected;
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
                Debug.LogWarning("Couldn't find the appropriate UI parent!");
                bigDisplay.transform.SetParent(this.transform);
            }
            bigDisplay.GetComponent<CardDisplayLarge>().SetCard(this.card);
        }

        if (selectable && eventData.button == PointerEventData.InputButton.Left) {
            SetSelected(!selected);
        }
    }

}
