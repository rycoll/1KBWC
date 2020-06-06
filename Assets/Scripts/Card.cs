using System;
using System.Collections.Generic;
using UnityEngine;

public class Card {

    protected CardData Info;

    private Sprite cardArt = null;

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
    public string GetCommaSpaceSeparatedTags () {
        return String.Join(", ", Info.GetTags().ToArray());
    }

    public Sprite GetSprite() {
        if (cardArt == null) {
            cardArt = Sprite.Create(
                Info.ArtTexture,
                new Rect(0, 0, 300, 300),
                new Vector2(0.5f, 0.5f)
            );
        }
        return cardArt;
    }
}
