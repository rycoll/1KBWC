using System;

[System.Serializable]
public class RE_SetDrawSize : RegularEffect {

    private RunTimeValue Player { get; set; }
    private RunTimeValue Size { get; set; }

    public RE_SetDrawSize (RunTimeValue player, RunTimeValue size) {
        Player = player;
        Size = size;
    }

    public override void Run(GameController gameController) {
        GamePlayer player = Player.Evaluate() as GamePlayer;
        if (CheckTypeError(Player, player)) return;
        player.DrawPerTurn = (int) Size.Evaluate();
    }

    public override void HandleInput(object obj) {
        if (!IgnoreInput) {
            GamePlayer playerObj = obj as GamePlayer;
            if (playerObj != null) {
                Player = new RunTimeValue(playerObj);
                return;
            }
            try {
                Size = new RunTimeValue((int) obj);
                return;
            } catch (InvalidCastException) {}
        }
    }
}