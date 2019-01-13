using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Card_Gain1Point : Card {
    public Card_Gain1Point(GameController gameController) {
        cardName = "Gain A Point";
        overrideRuleText = "You get 1 point!";
        cardArt = null;

        QueryRequest currPlayerReq = new QueryRequest(
            new Q_ActivePlayer(), null
        );
        RunTimeValue player = new RunTimeValue(currPlayerReq);

        CardEffect GainPointEffect = new RE_IncrementPlayerPoints(player, new RunTimeValue(1));
        AddEffect(GainPointEffect);
    }
}