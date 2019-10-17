using UnityEngine;
using UnityEngine.EventSystems;

public class PlayCardZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
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
        Draggable droppedObj = eventData.pointerDrag.GetComponent<Draggable>();
        if (droppedObj)
        {
            droppedObj.parentToReturnTo = this.transform;
            CardDisplaySmall cardObj = eventData.pointerDrag.GetComponent<CardDisplaySmall>();
            if (cardObj)
            {
                gameController.PlayCard(gameController.GetLocalPlayer(), cardObj.card);
                DisplayCard(cardObj);
            }
        }
    }

    public void DisplayCard (CardDisplaySmall cardDisplay) {
        foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }
        cardDisplay.transform.SetParent(this.transform.parent);
        cardDisplay.transform.position = this.transform.position;
    }
}
