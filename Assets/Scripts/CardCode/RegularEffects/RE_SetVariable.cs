using UnityEngine;

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
            return;
        }
        gameController.SetVariable(keyStr, valueStr);
    }
}