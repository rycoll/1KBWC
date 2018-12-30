using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControlType {FOR_LOOP, NUM_LOOP, IF, CHOOSE_ITEM, CHOOSE_VALUE};

public abstract class ControlEffect : CardEffect {
    protected List<CardEffect> effects;
    protected ControlType Type;
    // loop: number to use, OR collection to iterate over
    // if: condition to evaluate (read/get two values, compare with specified operator)
    // choose: item set to pick from, value set to pick from

    public ControlEffect(List<CardEffect> effects) {
        this.effects = effects;
    }

    public ControlType GetControlType() {
        return this.Type;
    }

    public abstract List<CardEffect> Compile();
}