using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ControlEffect : CardEffect {
    protected List<CardEffect> effects;
    // loop: number to use, OR collection to iterate over
    // if: condition to evaluate (read/get two values, compare with specified operator)
    // choose: item set to pick from, value set to pick from

    public ControlEffect(List<CardEffect> effects) {
        this.effects = effects;
    }

    public abstract List<CardEffect> Compile();
}