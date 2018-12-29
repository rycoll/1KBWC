using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CE_ForLoop : ControlEffect {
    private List<object> list;
    public CE_ForLoop(List<CardEffect> effects, List<object> objList) : base(effects) {
        this.Type = ControlType.FOR_LOOP;
        this.list = objList;
    }

    public List<CardEffect> Compile () {
        List<CardEffect> returnList = new List<CardEffect>();
        foreach (object obj in list) {
            foreach (CardEffect effect in effects) {
                CardEffect newEffect = effect.GetClone();
                newEffect.AddInput(obj);
                returnList.Add(newEffect);
            }
        }
        return returnList;
    }
}