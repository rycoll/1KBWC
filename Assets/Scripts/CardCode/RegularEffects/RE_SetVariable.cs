using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class RE_SetVariable : RegularEffect {

    private RunTimeValue Key { get; set; }
    private RunTimeValue Value { get; set; }

    public RE_SetVariable (RunTimeValue key, RunTimeValue value) {
        Key = key;
        Value = value;
    }

    public override void Run (GameController gameController) {
        string keyStr = Key.Evaluate() as string;
        string valueStr = Value.Evaluate() as string;
        if (keyStr == null || valueStr == null) {
            Debug.Log("Bad string evaluation :'''(");
            Done(gameController);
        } else {
            gameController.SetVariable(keyStr, valueStr);
        }

        Done(gameController);
    }

    public static EffectData GetEffectData () {
        return new EffectData() {
            name = "Set Variable",
            desc = "Set a global variable (which can be checked by Control Effects!).",
            fields = new List<FieldType>(){FieldType.STRING, FieldType.STRING},
            takesSubEffects = false
        };
    }
}