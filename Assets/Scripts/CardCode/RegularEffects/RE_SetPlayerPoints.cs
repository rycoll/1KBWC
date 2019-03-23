using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class RE_SetPlayerPoints : RegularEffect {

    private RunTimeValue<GamePlayer> Player { get; set; }
    private RunTimeValue<int> Points { get; set; }

    public RE_SetPlayerPoints (RunTimeValue<GamePlayer> player, RunTimeValue<int> points) {
        Player = player;
        Points = points;
    }

    public override void Run(GameController gameController) {
        GamePlayer player = Player.Evaluate(gameController);
        int points = Points.Evaluate(gameController);
        gameController.SetPlayerPoints(player, points);

        Done(gameController);
    }

    public static EffectData GetEffectData () {
        return new EffectData() {
            name = "Set Player Points",
            desc = "Set a player's point total to a new value.",
            fields = new List<FieldData>(){
                FieldLibrary.GetPlayerFieldData(), 
                FieldLibrary.GetNumberFieldData()
            },
            takesSubEffects = false,
            //effect = new RE_SetPlayerPoints()
        };
    }
}