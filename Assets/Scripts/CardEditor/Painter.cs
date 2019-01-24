using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Painter : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Image image;
    private RectTransform rect;
    public Color paintColour;
    public int brushSize;

    private Texture2D texBuffer;

    int width = 300;
    int height = 300;

    private int prevX, prevY;

    private void Start () {
        rect = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        
        texBuffer= new Texture2D(width, height);
        image.material.mainTexture = texBuffer;
    }

    private Vector2 GetMouseCoord(PointerEventData eventData) {
        Vector2 localCursor;
        bool isHit = RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rect, eventData.position, eventData.pressEventCamera, out localCursor
        );
        if (isHit) {
            return localCursor;
        } else {
            return new Vector2(-1, -1);
        }
    }

    public void OnBeginDrag(PointerEventData eventData){
        Vector2 localCursor = GetMouseCoord(eventData);
        prevX = (int)localCursor.x;
        prevY = (int)localCursor.y;
    }

    public void OnDrag(PointerEventData eventData){
        Vector2 localCursor;
        bool isHit = RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rect, eventData.position, eventData.pressEventCamera, out localCursor
        );
        if (!isHit) {
            return;
        }
        
        int xCoord = (int)(localCursor.x);
        int yCoord = (int)(localCursor.y);

        Paint(xCoord, yCoord);

        float m = (float)(yCoord - prevY) / (float)(xCoord - prevX);
        float c = yCoord - m * xCoord;

        if (xCoord < prevX) {
            for (int x = xCoord; x < prevX; x++) {
                Paint(x, (int)(m * x + c));
            }
        } else {
            for (int x = prevX; x < xCoord; x++) {
                Paint(x, (int)(m * x + c));
            }
        }

        prevX = xCoord;
        prevY = yCoord;
    }

    public void Paint(int x, int y) {
        Debug.Log(x + ", " + y);

        if (x < 0 || x >= width || y < 0 || y >= height) {
            return;
        }

        Color[] colours = texBuffer.GetPixels();
        
        for (int i = x - brushSize; i <= x + brushSize; i++) {
            for (int j = y - brushSize; j <= y + brushSize; j++) {
                if (!(i < 0 || i >= width || j < 0 || j >= height)) {
                    colours[j * height + i] = paintColour;
                }
            }
        }
        
        texBuffer.SetPixels(colours);

        image.material.mainTexture = texBuffer;
        texBuffer.Apply();
    }

    public void OnEndDrag(PointerEventData eventData){}
}
