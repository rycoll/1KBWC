using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Gain1Point : Card {
    public Card_Gain1Point(GameController gameController) {
        cardName = "Gain A Point";
        overrideRuleText = "You get 1 point!";
        cardArt = null;

        QueryRequest currPlayer = new QueryRequest(
            ScriptableObject.CreateInstance<Q_ActivePlayer>(), null
        );
        RunTimeValue player = new RunTimeValue(currPlayer);
        QueryRequest reqCurrPoints = new QueryRequest(
            ScriptableObject.CreateInstance<Q_PlayerPoints>(), player
        );
        int currPoints = (int) gameController.RunQuery(reqCurrPoints).GetReturnValue();
        RunTimeValue newPoints = new RunTimeValue(currPoints + 1);

        CardEffect GainPointEffect = new RE_SetPlayerPoints(player, newPoints);
        AddEffect(GainPointEffect);
    }
}