using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CE_If : ControlEffect {
    private List<Condition> conditions;
    public CE_If(List<CardEffect> effects, List<Condition> conditionList) : base(effects) {
        this.conditions = conditionList;
    }

    // at the moment, all conditions need to pass in order for the effects to be executed
    public override List<CardEffect> Compile () {
        foreach (Condition condition in conditions) {
            if (!condition.Evaluate()) {
                return new List<CardEffect>();
            }
        }
        return this.effects;
    }

    public static EffectData GetEffectData () {
        return new EffectData() {
            name = "If... Then...",
            desc = "Run effects ONLY IF a condition is met.",
            fields = new List<FieldType>(){FieldType.CONDITION},
            takesSubEffects = true
        };
    }
}