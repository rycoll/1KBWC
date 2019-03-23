using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ControlEffect : CardEffect {
    protected List<CardEffect> effects;

    public ControlEffect(List<CardEffect> effects) {
        this.effects = effects;
    }

    public abstract List<CardEffect> Compile(GameController gameController);
}