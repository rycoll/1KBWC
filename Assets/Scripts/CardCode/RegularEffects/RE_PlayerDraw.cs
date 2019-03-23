using System;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class RE_PlayerDraw : RegularEffect {

    private RunTimeValue<GamePlayer> Player { get; set; }
    private RunTimeValue<int> NumCards { get; set; }

    public RE_PlayerDraw (RunTimeValue<GamePlayer> player, RunTimeValue<int> num) {
        Player = player;
        NumCards = num;
    }
    public override void Run(GameController gameController) {
        GamePlayer player = Player.Evaluate(gameController);
        int num = NumCards.Evaluate(gameController);

        for (int i = 0; i < num; i++) {
            gameController.PlayerDrawCard(player);
        }

        Done(gameController);
    }

    public static EffectData GetEffectData () {
        return new EffectData() {
            name = "Draw Cards",
            desc = "A player draws X cards from the deck.",
            fields = new List<FieldData>(){
                FieldLibrary.GetPlayerFieldData(), 
                FieldLibrary.GetNumberFieldData()
            },
            takesSubEffects = false,
            //effect = new RE_PlayerDraw()
        };
    }
}