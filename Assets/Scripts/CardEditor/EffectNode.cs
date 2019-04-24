using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectNode : MonoBehaviour
{
    private CardEffect effect;
    private bool takesSubEffects = false;

    public void SetData (EffectData data) {
        this.effect = data.effect;
        this.takesSubEffects = data.takesSubEffects;
    }

    public void Evaluate () {
        if (effect == null) {
            return;
        }
        foreach (Transform child in transform) {
            FieldNode field = child.gameObject.GetComponent<FieldNode>();
            if (field) {
                ProcessFieldNode(field);
            } else if (takesSubEffects) {
                EffectNode subEffect = child.gameObject.GetComponent<EffectNode>();
                if (subEffect) {
                    ProcessEffectNode(subEffect);
                }
            }
        }
    }

    public void ProcessFieldNode (FieldNode node) {
        
    }

    public void ProcessEffectNode (EffectNode node) {

    }
}
