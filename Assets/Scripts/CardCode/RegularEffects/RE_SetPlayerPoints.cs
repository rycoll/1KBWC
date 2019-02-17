using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class RE_SetPlayerPoints : RegularEffect {

    private RunTimeValue Player { get; set; }
    private RunTimeValue Points { get; set; }

    public RE_SetPlayerPoints (RunTimeValue player, RunTimeValue points) {
        Player = player;
        Points = points;
    }

    public override void Run(GameController gameController) {
        GamePlayer player = Player.Evaluate() as GamePlayer;
        if (CheckTypeError(Player, player)) return;
        int points = (int) Points.Evaluate();
        gameController.SetPlayerPoints(player, points);

        Done(gameController);
    }

    public override void HandleInput(object obj) {
        if (!IgnoreInput) {
            GamePlayer playerObj = obj as GamePlayer;
            if (playerObj != null) {
                Player = new RunTimeValue(playerObj);
                return;
            }
            try {
                Points = new RunTimeValue((int) obj);
                return;
            } catch (InvalidCastException) {}
        }
    }

    public static EffectData GetEffectData () {
        return new EffectData() {
            name = "Set Player Points",
            desc = "Set a player's point total to a new value.",
            fields = new List<FieldData>(){
                FieldLibrary.GetPlayerFieldData(), 
                FieldLibrary.GetNumberFieldData()
            },
            takesSubEffects = false
        };
    }
}