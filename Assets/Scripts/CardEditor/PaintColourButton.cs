using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintColourButton : MonoBehaviour
{
    public static Painter painter;

    public Color colour;

    public void SetPainterColour() {
        painter.paintColour = colour;
    }

    private void Start () {
        GetComponent<Image>().color = colour;
    }
}
