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
}
