using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CE_ForLoop : ControlEffect {
    private RunTimeValue list;
    public CE_ForLoop(List<CardEffect> effects, RunTimeValue objList) : base(effects) {
        this.list = objList;
    }

    public override List<CardEffect> Compile () {
        List<CardEffect> returnList = new List<CardEffect>();
        List<object> objectList = RunTimeValue.TryExtractObjectList(list);
        if (objectList == null) {
            return new List<CardEffect>();
        }
        foreach (object obj in objectList) {
            foreach (CardEffect effect in effects) {
                CardEffect newEffect = effect.GetClone();
                newEffect.HandleInput(obj);
                returnList.Add(newEffect);
            }
        }
        return returnList;
    }

    public static EffectData GetEffectData () {
        return new EffectData() {
            name = "For Each (Loop)",
            desc = "Run effects once for each item in a list (e.g. 'For each player..., that player...').",
            fields = new List<FieldData>(){
                FieldLibrary.ListFieldData
            },
            takesSubEffects = true
        };
    }
}