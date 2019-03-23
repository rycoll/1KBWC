using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class RE_IncrementPlayerPoints : RegularEffect {

    private RunTimeValue<GamePlayer> Player { get; set; }
    private RunTimeValue<int> Points { get; set; }

    public RE_IncrementPlayerPoints (RunTimeValue<GamePlayer> player, RunTimeValue<int> points) {
        Player = player;
        Points = points;
    }

    public override void Run(GameController gameController) {
        GamePlayer player = Player.Evaluate(gameController);
        int points = Points.Evaluate(gameController);
        gameController.SetPlayerPoints(player, player.Points + points);

        Done(gameController);
    }

    public static EffectData GetEffectData () {
        return new EffectData() {
            name = "Increment Player Points",
            desc = "Add a value to a player's point total. To subtract, use a negative value.",
            fields = new List<FieldData>(){
                FieldLibrary.GetPlayerFieldData(), 
                FieldLibrary.GetNumberFieldData()
            },
            takesSubEffects = false,
            //effect = new RE_IncrementPlayerPoints()
        };
    }
}