using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectTextUI : EffectUIElement
{
    private Text text;

    public void Start () {
        this.text = GetComponent<Text>();
    }

    public void SetText (string text) {
        this.text.text = text;
    }
}
