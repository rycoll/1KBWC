using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayCardZone : MonoBehaviour
{
    private GameObject cardDisplay;
    public GameController gameController;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
        {
            return;
        }
        Draggable droppedObj = eventData.pointerDrag.GetComponent<Draggable>();
        if (droppedObj)
        {
            droppedObj.placeholderParent = this.transform;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
        {
            return;
        }
        Draggable droppedObj = eventData.pointerDrag.GetComponent<Draggable>();
        if (droppedObj && droppedObj.placeholderParent == this.transform)
        {
            droppedObj.placeholderParent = this.transform;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Dropped on " + this.gameObject.name);
        Draggable droppedObj = eventData.pointerDrag.GetComponent<Draggable>();
        if (droppedObj)
        {
            droppedObj.parentToReturnTo = this.transform;
            CardDisplaySmall cardObj = eventData.pointerDrag.GetComponent<CardDisplaySmall>();
            if (cardObj)
            {
                gameController.PlayCard(gameController.GetLocalPlayer(), cardObj.card);
                cardDisplay = cardObj.bigDisplayPrefab;
            }
        }
    }
}
