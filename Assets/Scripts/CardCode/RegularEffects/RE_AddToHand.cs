using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class RE_AddToHand : RegularEffect {

    private RunTimeValue Player { get; set; }
    private RunTimeValue Card { get; set; }

    public RE_AddToHand (RunTimeValue player, RunTimeValue card) {
        Player = player;
        Card = card;
    }
    public override void Run(GameController gameController) {
        GamePlayer player = Player.Evaluate() as GamePlayer;
        Card card = Card.Evaluate() as Card;

        if (CheckTypeError(Player, player)) return;
        if (CheckTypeError(Card, card)) return;
        
        gameController.GiveCardToPlayer(player, card);

        Done(gameController);
    }

    public override void HandleInput(object obj) {
        if (!IgnoreInput) {
            GamePlayer playerObj = obj as GamePlayer;
            if (playerObj != null) {
                Player = new RunTimeValue(playerObj);
                return;
            }
            Card cardObj = obj as Card;
            if (cardObj != null) {
                Card = new RunTimeValue(cardObj);
            }
        }
    }

    public static EffectData GetEffectData () {
        return new EffectData() {
            name = "Add Card to Hand",
            desc = "Add a card to a player's hand.",
            fields = new List<FieldType>(){FieldType.PLAYER, FieldType.CARD},
            takesSubEffects = false
        };
    }
}