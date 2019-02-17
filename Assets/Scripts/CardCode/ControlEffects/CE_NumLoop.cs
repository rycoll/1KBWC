using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CE_NumLoop : ControlEffect {
    private int numLoops;
    public CE_NumLoop(List<CardEffect> effects, RunTimeValue n) : base(effects) {
        this.numLoops = (int) n.Evaluate();
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

    public static EffectData GetEffectData () {
        return new EffectData() {
            name = "Loop",
            desc = "Run effects X times",
            fields = new List<FieldData>(){
                FieldLibrary.GetNumberFieldData()
            },
            takesSubEffects = true
        };
    }
}