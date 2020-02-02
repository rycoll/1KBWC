using System;
using System.Collections.Generic;
using UnityEngine;

public class Card {

    protected CardData Info;

    // cardArt ought to be a Sprite, but that's not serializable :/
    public object cardArt;


    private CardZone zone;
    public CardZone Zone { get; set; }

    // this constructor is just for testing
    public Card () {
        Info = new CardData();
    }
    public Card (CardData data) {
        Info = data;
    }

    public string GetID () {
        return Info.GetID();
    }
    public string GetName() {
        return Info.Name;
    }

    public string GetRulesText ()
    {
        return Info.GetRulesText();
    }

    public List<byte> GetEffectBytes () {
        return Info.GetEffectBytes();
    }

    public bool HasTag (string tag) {
        return Info.HasTag(tag);
    }
}
