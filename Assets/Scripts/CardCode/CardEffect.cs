using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class CardEffect
{
    protected bool IgnoreInput;

    public CardEffect GetClone () {
        // there's a pretty decent chance this doesn't work at all
        CardEffect clone = (CardEffect) this.MemberwiseClone();
        return clone;
    }

    public virtual void HandleInput (object obj) {}

    public void SetIgnoreInput (bool b) {
        IgnoreInput = b;
    }

    protected CardEffect nextEffect = null;
    public void SetNextEffect (CardEffect effect) {
        nextEffect = effect;
    }
    public CardEffect GetNextEffect () {
        if (this.nextEffect == null) {
            return null;
        }
        return nextEffect;
    }

    public void RunNextEffect () {
        if (nextEffect != null) EffectExecutor.Execute(nextEffect);
    }

    public void Done (GameController game) {
        if (GetNextEffect() == null) {
            game.Next();
        } else {
            RunNextEffect();
        }
    }

}
