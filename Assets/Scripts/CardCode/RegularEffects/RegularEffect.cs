using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class RegularEffect : CardEffect {
    public abstract void Run(GameController gameController);

    protected bool CheckTypeError (RunTimeValue value, object castObj) {
        if (castObj == null) {
            Debug.Log("A RunTimeValue evaluated to an unexpected type!");
            Debug.Log(value.Evaluate());
            return true;
        }
        return false;
    }

}