using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardData {

    public string compilerVersion { get; set; }

    public string Name { get; set; }
    private string rulesText;
    private List<string> Tags = new List<string>();
    private List<byte> Effects = new List<byte>();

    public Texture2D ArtTexture { get; set; }

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
