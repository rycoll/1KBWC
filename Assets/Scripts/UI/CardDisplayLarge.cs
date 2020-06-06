using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardDisplayLarge : MonoBehaviour, IPointerExitHandler
{
    public Card card;
    public Text titleText;
    public Image cardImage;
    public Text rulesText;
    public Scrollbar rulesScrollbar;

    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(this.gameObject);
    }

    public void SetCard(Card card)
    {
        this.card = card;
        RefreshDisplay();
    }

    private void RefreshDisplay()
    {
        string tags = card.GetCommaSpaceSeparatedTags();

        titleText.text = card.GetName();
        cardImage.sprite = card.GetSprite();
        rulesText.text = tags + "\n" + card.GetRulesText();
        rulesScrollbar.value = 1f;
    }

}
