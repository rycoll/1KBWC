using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Card {

    public string cardName;
    // cardArt ought to be a Sprite, but that's not serializable :/
    public object cardArt;
    private string generatedRuleText = null;
    public string overrideRuleText = null;
    public List<string> Tags { get; set; }
    public List<CardEffect> Effects { get; set; }

    public string GetRuleText ()
    {
        // if override text has been set, return it
        // otherwise return generated rule text
        return (string.IsNullOrEmpty(generatedRuleText)) ? overrideRuleText : generatedRuleText;
    }

    public void AddTag (string tag) {
        Tags.Add(tag);
    }

    public void RemoveTag (string tag) {
        Tags.Remove(tag);
    }

	public void AddEffect (CardEffect effect) {
        if (Effects == null) {
            Effects = new List<CardEffect>();
        }
        Effects.Add(effect);
    }
}
