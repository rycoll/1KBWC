using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Card_Gain1Point : Card {
    public Card_Gain1Point(GameController gameController) {
        cardName = "Gain A Point";
        overrideRuleText = "You get 1 point!";
        cardArt = null;

        QueryRequest<GamePlayer> currPlayerReq = new QueryRequest<GamePlayer>(
            new Q_ActivePlayer()
        );
        RunTimeValue<GamePlayer> player = new RunTimeValue<GamePlayer>(currPlayerReq);

        CardEffect GainPointEffect = new RE_IncrementPlayerPoints(player, new RunTimeValue<int>(1));
        AddEffect(GainPointEffect);
    }
}