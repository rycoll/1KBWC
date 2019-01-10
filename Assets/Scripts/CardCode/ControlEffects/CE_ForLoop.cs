using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CE_ForLoop : ControlEffect {
    private List<object> list;
    public CE_ForLoop(List<CardEffect> effects, RunTimeValue objList) : base(effects) {
        this.list = RunTimeValue.TryExtractObjectList(objList);
        if (this.list == null) {
            this.list = new List<object>();
        }
    }

    public override List<CardEffect> Compile () {
        List<CardEffect> returnList = new List<CardEffect>();
        foreach (object obj in list) {
            foreach (CardEffect effect in effects) {
                CardEffect newEffect = effect.GetClone();
                newEffect.HandleInput(obj);
                returnList.Add(newEffect);
            }
        }
        return returnList;
    }
}