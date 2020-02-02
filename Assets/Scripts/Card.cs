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
    public List<byte> Effects { get; set; }

    private CardZone zone;
    public CardZone Zone { get; set; }

    public Card () {
        GenerateID();
    }

    private string id;
    public void GenerateID () {
        id = System.Guid.NewGuid().ToString();
    }
    public string GetID () {
        return id;
    }

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

    public bool HasTag (string tag) {
        return Tags.Contains(tag);
    }

    public void AddEffect(byte[] effect) {
        AddEffect(new List<byte>(effect));
    }
	public void AddEffect (List<byte> effect) {
        if (Effects == null) {
            Effects = new List<byte>();
        }
        Effects.AddRange(effect);
    }
}
