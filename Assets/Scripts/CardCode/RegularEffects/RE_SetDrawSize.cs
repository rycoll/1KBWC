public class RE_SetDrawSize : RegularEffect {

    private RunTimeValue Player { get; set; }
    private RunTimeValue Size { get; set; }

    public RE_SetPlayerPoints (RunTimeValue player, RunTimeValue size) {
        Player = player;
        Size = size;
    }

    public override void Run(GameController gameController) {
        GamePlayer player = Player.Evaluate() as GamePlayer;
        if (CheckTypeError(Player, player)) return;
        player.DrawPerTurn = (int) Points.Evaluate();
    }
}