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
        player.Points = (int) Points.Evaluate();
    }
}