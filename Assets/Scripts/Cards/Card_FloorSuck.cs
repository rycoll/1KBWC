using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Card_FloorSuck : Card {
    public Card_FloorSuck(GameController game) {
        cardName = "Floor Suck";
        overrideRuleText = "If the arena is wet, gain 40 points.";
        cardArt = null;

        RunTimeValue checkArenaFlag = new RunTimeValue(
            ScriptableObject.CreateInstance<Q_Variable>(), "Arena"
        );
        Condition checkArenaWetness = new Condition(
            checkArenaFlag,
            new RunTimeValue("WET"),
            ConditionOperator.EQUAL
        );

        List<Condition> conditions = new List<Condition>();
        conditions.Add(checkArenaWetness);

        QueryRequest currPlayerReq = new QueryRequest(
            ScriptableObject.CreateInstance<Q_ActivePlayer>(), null
        );
        RunTimeValue player = new RunTimeValue(currPlayerReq);
        CardEffect gainPointEffect = new RE_IncrementPlayerPoints(player, new RunTimeValue(40));
        
        List<CardEffect> effects = new List<CardEffect>();
        effects.Add(gainPointEffect);

        CardEffect pointsIfWet = new CE_If(effects, conditions);
        AddEffect(pointsIfWet);
    }
}