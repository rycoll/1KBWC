using System;
using System.Collections;

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
}