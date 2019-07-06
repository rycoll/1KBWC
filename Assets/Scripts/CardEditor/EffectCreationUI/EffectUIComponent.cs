using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectUIComponent : MonoBehaviour
{

    private EffectUIComponent uiParent;
    private Color colour;

    public void SetParent (EffectUIComponent p) {
        this.uiParent = p;
    }

    public int GetDepth () {
        if (uiParent == null) {
            return 1;
        }
        return 1 + uiParent.GetDepth();
    }

    public Color GetRandColorThatContrastsWithBlack () {
        Color c;
        do {
            c = Random.ColorHSV();
        } while ((c.r*0.299 + c.g*0.587 + c.b*0.114) < 0.5);
        return c;
    }

    public void SetColour () {
        if (uiParent == null) {
            colour = GetRandColorThatContrastsWithBlack(); 
        } else {
            colour = uiParent.GetColour();
        }
    }
    public Color GetColour () {
        if (colour == null) {
            SetColour();
        }
        return colour;
    }
    
}
