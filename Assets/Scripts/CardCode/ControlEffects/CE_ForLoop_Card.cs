using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CE_ForLoop_Card : ControlEffect {
    private RunTimeValue<List<Card>> list;
    public CE_ForLoop_Card(List<CardEffect> effects, RunTimeValue<List<Card>> objList) : base(effects) {
        this.list = objList;
    }

    

    public override List<CardEffect> Compile (GameController gameController) {
        List<CardEffect> returnList = new List<CardEffect>();
        List<Card> items = list.Evaluate(gameController);
        if (items != null) {    
            foreach (Card card in items) {
                foreach (CardEffect effect in effects) {
                    CardEffect newEffect = effect.GetClone();
                    
                    returnList.Add(newEffect);
                }
            }
        }
        return returnList;
    }

    public static EffectData GetEffectData () {
        return new EffectData() {
            name = "For Each (Loop)",
            desc = "Run effects once for each item in a list (e.g. 'For each player..., that player...').",
            fields = new List<FieldData>(){
                FieldLibrary.GetListFieldData()
            },
            takesSubEffects = true,
            //effect = new CE_ForLoop()
        };
    }
}