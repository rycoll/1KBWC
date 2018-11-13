using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

    public Card.Location zoneType;

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
                cardObj.card.cardLocation = this.zoneType;
            }
        }
    }
	
}
