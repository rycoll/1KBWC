using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CE_NumLoop : ControlEffect {
    private int numLoops;
    public CE_NumLoop(List<CardEffect> effects, int n) : base(effects) {
        this.Type = ControlType.FOR_LOOP;
        this.numLoops = n;
    }

    public override List<CardEffect> Compile () {
        List<CardEffect> returnList = new List<CardEffect>();
        for (int i = 0; i < numLoops; i++) {
            foreach (CardEffect effect in effects) {
                CardEffect newEffect = effect.GetClone();
                returnList.Add(newEffect);
            }
        }
        return returnList;
    }
}