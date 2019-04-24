using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class RE_AddToHand : RegularEffect {

    public RE_AddToHand (RunTimeValue<GamePlayer> player, RunTimeValue<Card> card) {
        Fields.TargetPlayer = player;
        Fields.TargetCard = card;
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