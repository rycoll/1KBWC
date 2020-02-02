using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Painter : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    private Image image;
    private RectTransform rect;
    public Color paintColour;
    private int brushSize;
    private bool fillMode;

    private Texture2D texBuffer;

    int width = 300;
    int height = 300;

    public Texture2D ExportTexture () {
        return texBuffer;
    }

    private int prevX, prevY = -1;

    private void Start () {
        rect = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        texBuffer = new Texture2D(width, height);

        // set all pixels to white to start
        Color[] colours = texBuffer.GetPixels();
        for (int i = 0; i < colours.Length; i++) {
            colours[i] = Color.white;
        }
        texBuffer.SetPixels(colours);
        image.material.mainTexture = texBuffer;
        texBuffer.Apply();

        brushSize = 1;
        fillMode = false;

        PaintColourButton.painter = this;
    }

    public void SetBrushSize(float n) {
        brushSize = (int) n;
    }

    public void SetFillMode(bool b) {
        fillMode = b;
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

    public struct Coord {
        public int x;
        public int y;
        public Coord (int i, int j) {
            x = i;
            y = j;
        }
    }

    private void Fill (int x, int y) {
        Color[] colours = texBuffer.GetPixels();
        Color targetColour = colours[y * height + x];
        List<Coord> queue = new List<Coord>();
        bool[,] visited = new bool[width,height];
        
        queue.Add(new Coord(x, y));
        while (queue.Count > 0) {
            Coord curr = queue[0];
            queue.RemoveAt(0);
            if (visited[curr.x, curr.y]) {
                continue;
            }
            visited[curr.x, curr.y] = true;
            colours[curr.y * height + curr.x] = paintColour;
            Coord[] neighbours = new Coord[]{
                new Coord(curr.x+1, curr.y),
                new Coord(curr.x-1, curr.y),
                new Coord(curr.x, curr.y+1),
                new Coord(curr.x, curr.y-1)
            };
            foreach (Coord coord in neighbours) {
                if (coord.x >= 0 && coord.x < width && coord.y >= 0 && coord.y < height) {
                    if (!(visited[coord.x, coord.y])) {
                        Color coordColour = colours[coord.y * height + coord.x];
                        if (coordColour == targetColour) {
                            queue.Insert(0, coord);
                        }
                    } 
                }
            }
        }

        texBuffer.SetPixels(colours);
        image.material.mainTexture = texBuffer;
        texBuffer.Apply();
    }

    public void OnPointerClick(PointerEventData eventData){
        Vector2 localCursor = GetMouseCoord(eventData);
        int x = (int)localCursor.x;
        int y = (int)localCursor.y;
        if (fillMode) {
            Fill(x, y);
        } else {
            Paint(x, y);
        }
    }

    public void OnBeginDrag(PointerEventData eventData) {}

    public void OnDrag(PointerEventData eventData){
        if (fillMode) {
            return;
        }
        Vector2 localCursor;
        bool isHit = RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rect, eventData.position, eventData.pressEventCamera, out localCursor
        );
        if (!isHit) {
            return;
        }
        
        int xCoord = (int)(localCursor.x);
        int yCoord = (int)(localCursor.y);

        if (prevX == -1 || prevY == -1) {
            Paint(xCoord, yCoord);
        } else {
            float m = (float)(yCoord - prevY) / (float)(xCoord - prevX);
            float c = yCoord - m * xCoord;
            List<Coord> coords = new List<Coord>();
            if (xCoord < prevX) {
                for (int x = xCoord; x < prevX; x++) {
                    coords.Add(new Coord(x, (int)(m * x + c)));
                }
            } else if (xCoord > prevX) {
                for (int x = prevX; x < xCoord; x++) {
                    coords.Add(new Coord(x, (int)(m * x + c)));
                }
            } else if (xCoord == prevX) {
                if (yCoord < prevY) {
                    for (int y = yCoord; y < prevY; y++) {
                        coords.Add(new Coord(xCoord, y));
                    }
                } else {
                    for (int y = prevY; y < yCoord; y++) {
                        coords.Add(new Coord(xCoord, y));
                    }
                }
            }
            PaintGroup(coords);
        }

        prevX = xCoord;
        prevY = yCoord;
    }

    public void Paint(int x, int y) {
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

    public void PaintGroup(List<Coord> coords) {
        Color[] colours = texBuffer.GetPixels();
        foreach (Coord coord in coords) {
            if (coord.x < 0 || coord.x >= width || coord.y < 0 || coord.y >= height) {
                continue;
            }
            for (int i = coord.x - brushSize; i <= coord.x + brushSize; i++) {
                for (int j = coord.y - brushSize; j <= coord.y + brushSize; j++) {
                    if (!(i < 0 || i >= width || j < 0 || j >= height)) {
                        colours[j * height + i] = paintColour;
                    }
                }
            }
        }
        texBuffer.SetPixels(colours);
        image.material.mainTexture = texBuffer;
        texBuffer.Apply();
    }

    public void OnEndDrag(PointerEventData eventData){
        prevX = -1;
        prevY = -1;
    }
}
