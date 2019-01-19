using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class Card_Gift : Card {
    public Card_Gift(GameController game) {
        cardName = "Gift";
        overrideRuleText = "Choose another player. That player draws a card.";
        cardArt = null;

        GamePlayer[] opponentsArr = game.GetOpponents();
        List<GamePlayer> playerList = new List<GamePlayer>(opponentsArr);
        List<object> objectList = playerList.Cast<object>().ToList();

        CardEffect drawEffect = new RE_PlayerDraw(new RunTimeValue(null), new RunTimeValue(1));
        CardEffect choiceEffect = new RE_Choice(
            new RunTimeValue(objectList),
            new RunTimeValue(drawEffect)
        );

        AddEffect(choiceEffect);
        AddEffect(drawEffect);
    }
}