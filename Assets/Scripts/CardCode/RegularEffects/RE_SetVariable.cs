using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class RE_SetVariable : RegularEffect {

    private RunTimeValue<string> Key { get; set; }
    private RunTimeValue<string> Value { get; set; }

    public RE_SetVariable (RunTimeValue<string> key, RunTimeValue<string> value) {
        Key = key;
        Value = value;
    }

    public override void Run (GameController gameController) {
        string keyStr = Key.Evaluate(gameController);
        string valueStr = Value.Evaluate(gameController);
        gameController.SetVariable(keyStr, valueStr);
        Done(gameController);
    }

    public static EffectData GetEffectData () {
        return new EffectData() {
            name = "Set Variable",
            desc = "Set a global variable (which can be checked by Control Effects!).",
            fields = new List<FieldData>(){
                FieldLibrary.GetStringFieldData(), 
                FieldLibrary.GetStringFieldData()
            },
            takesSubEffects = false,
            //effect = new RE_SetVariable()
        };
    }
}