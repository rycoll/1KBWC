using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card {

    public string cardName;
    public Sprite cardArt;
    private string generatedRuleText = null;
    public string overrideRuleText = null;
    public enum Location {HAND, FIELD, DECK, DISCARD};
    public Location cardLocation;

    public string GetRuleText ()
    {
        // if override text has been set, return it
        // otherwise return generated rule text
        return (string.IsNullOrEmpty(generatedRuleText)) ? overrideRuleText : generatedRuleText;
    }

	public abstract EffectResult Execute ();
}
