using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class CardEffect
{
    protected bool IgnoreInput;

    protected EffectFieldContainer Fields;

    public CardEffect GetClone () {
        // there's a pretty decent chance this doesn't work at all
        CardEffect clone = (CardEffect) this.MemberwiseClone();
        return clone;
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
