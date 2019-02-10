using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectInfoUI : MonoBehaviour
{
    public Text effectNameText;
    public Text effectDescText;

    public void UpdateDisplay (EffectData data) {
        effectNameText.text = data.name;
        effectDescText.text = data.desc;
    }

    public void RemoveEffect () {
        Destroy(transform.parent.gameObject);
    }
}
