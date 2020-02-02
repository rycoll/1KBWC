using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardData {

    public string Name {get; set; }
    private string rulesText;
    public List<string> Tags = new List<string>();
    public List<byte> Effects { get; set; }

    public CardData () {
        GenerateID();
    }

    private string id;
    public void GenerateID () {
        id = System.Guid.NewGuid().ToString();
    }
    public string GetID () {
        return id;
    }

    public string GetRulesText ()
    {
        if (string.IsNullOrEmpty(rulesText)) {
            RulesTextInterpreter RTI = new RulesTextInterpreter(Effects);
            rulesText = RTI.GetFullRulesText();
        }
        return rulesText;
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
    public List<byte> GetEffectBytes () {
        return new List<byte>(Effects.ToArray());
    }
}
