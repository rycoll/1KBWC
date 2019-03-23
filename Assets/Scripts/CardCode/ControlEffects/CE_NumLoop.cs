using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CE_NumLoop : ControlEffect {
    private RunTimeValue<int> numLoops;
    public CE_NumLoop(List<CardEffect> effects, RunTimeValue<int> n) : base(effects) {
        this.effects = effects;
        this.numLoops = n;
    }

    public override List<CardEffect> Compile (GameController gameController) {
        List<CardEffect> returnList = new List<CardEffect>();
        for (int i = 0; i < numLoops.Evaluate(gameController); i++) {
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
            takesSubEffects = true,
            //effect = new CE_NumLoop()
        };
    }
}