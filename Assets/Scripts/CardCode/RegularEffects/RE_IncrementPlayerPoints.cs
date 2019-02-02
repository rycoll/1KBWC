using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class RE_IncrementPlayerPoints : RegularEffect {

    private RunTimeValue Player { get; set; }
    private RunTimeValue Points { get; set; }

    public RE_IncrementPlayerPoints (RunTimeValue player, RunTimeValue points) {
        Player = player;
        Points = points;
    }

    public override void Run(GameController gameController) {
        GamePlayer player = Player.Evaluate() as GamePlayer;
        if (CheckTypeError(Player, player)) return;
        int points = (int) Points.Evaluate();
        gameController.SetPlayerPoints(player, player.Points + points);

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
            name = "Increment Player Points",
            desc = "Add a value to a player's point total. To subtract, use a negative value.",
            fields = new List<FieldType>(){FieldType.PLAYER, FieldType.NUMBER},
            takesSubEffects = false
        };
    }
}