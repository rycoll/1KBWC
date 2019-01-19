using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceItemDisplay : MonoBehaviour
{
    public Text title;
    public Text info;

    public void SetText(string t, string i) {
        title.text = t;
        info.text = i;
    }
}
