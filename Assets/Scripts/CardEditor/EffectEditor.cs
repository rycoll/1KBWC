using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectEditor : MonoBehaviour
{
    private List<EffectData> effects;

    public void Start() {
        effects = new List<EffectData>();

    }

    void OnGUI () {
        if (GUILayout.Button("Text"))
        {
            Debug.Log("Clicked");
        }
    }

    public List<EffectEditorDisplayChunk> GetDisplayData () {
        List<EffectEditorDisplayChunk> chunks = new List<EffectEditorDisplayChunk>();
        foreach (EffectData data in effects) {
            string effectText = data.name +"\n" + data.message;
            chunks.Add(
                new EffectEditorDisplayChunk(false, effectText)
            );
            // add buttons for fields

        }
        return chunks;
    }
}

public class EffectEditorDisplayChunk {
    public bool isButton;
    public string text;

    public EffectEditorDisplayChunk(bool button, string t) {
        isButton = button;
        text = t;
    }
}
