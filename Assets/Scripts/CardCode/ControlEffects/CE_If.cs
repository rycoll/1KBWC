using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CE_If : ControlEffect {
    private List<Condition> conditions;
    public CE_If(List<CardEffect> effects, List<Condition> conditionList) : base(effects) {
        this.Type = ControlType.IF;
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
}