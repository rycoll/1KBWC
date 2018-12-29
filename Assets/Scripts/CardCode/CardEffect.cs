using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardEffect
{
    protected Hashtable hash = new Hashtable();

    public void AddInput (object input) {
        hash.Add("input", input);
    }

    public CardEffect GetClone () {
        // there's a pretty decent chance this doesn't work at all
        CardEffect clone = (CardEffect) this.MemberwiseClone();
        clone.hash = (Hashtable) this.hash.Clone();
        return clone;
    }
}
