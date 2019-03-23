using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class RE_AddToHand : RegularEffect {

    private RunTimeValue<GamePlayer> Player { get; set; }
    private RunTimeValue<Card> Card { get; set; }

    public RE_AddToHand (RunTimeValue<GamePlayer> player, RunTimeValue<Card> card) {
        Player = player;
        Card = card;
    }
    public override void Run(GameController gameController) {
        GamePlayer player = Player.Evaluate(gameController);
        Card card = Card.Evaluate(gameController);
        
        gameController.GiveCardToPlayer(player, card);

        Done(gameController);
    }

    public static EffectData GetEffectData () {
        return new EffectData() {
            name = "Add Card to Hand",
            desc = "Add a card to a player's hand.",
            fields = new List<FieldData>(){
                FieldLibrary.GetPlayerFieldData(), 
                FieldLibrary.GetCardFieldData()
            },
            takesSubEffects = false,
            //effect = new RE_AddToHand()
        };
    }
}