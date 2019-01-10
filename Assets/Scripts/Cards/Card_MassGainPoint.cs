using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_MassGainPoint : Card {
    public Card_MassGainPoint(GameController gameController) {
        cardName = "Mass Gain Points";
        overrideRuleText = "Each player gains 2 points.";
        cardArt = null;

        CardEffect playerGainPoints = new RE_IncrementPlayerPoints(new RunTimeValue(null), new RunTimeValue(2));
        List<CardEffect> effectList = new List<CardEffect>();
        effectList.Add(playerGainPoints);

        QueryRequest requestPlayers = new QueryRequest(ScriptableObject.CreateInstance<Q_Players>());
        RunTimeValue playerList = new RunTimeValue(requestPlayers);

        CardEffect loop = new CE_ForLoop(effectList, playerList);

        AddEffect(loop);
    }
}