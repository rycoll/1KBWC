using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_TargetPlayerDrawsCard : Card {
    public Card_TargetPlayerDrawsCard () {
        cardName = "Gift";
        overrideRuleText = "Target player draws a card.";
        SetID();

        Effects = new List<byte>();
              
    }
}