using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TagItemDisplay : MonoBehaviour
{
    public Text text;

    public void SetTag (string tag) {
        text.text = tag;
    }

    public string GetTag () {
        return text.text;
    }

    public void Remove () {
        Destroy(this.gameObject);
    }
    
}
