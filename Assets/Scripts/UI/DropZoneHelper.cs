using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZoneHelper : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public DropZone dropZone;

    public void OnPointerEnter(PointerEventData eventData)
    {
        dropZone.OnPointerEnter(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        dropZone.OnPointerExit(eventData);
    }

    public void OnDrop(PointerEventData eventData)
    {
        dropZone.OnDrop(eventData);
    }

}
