public class RE_SetMaxHandSize : RegularEffect {

    private RunTimeValue Player { get; set; }
    private RunTimeValue Size { get; set; }

    public RE_SetMaxHandSize (RunTimeValue player, RunTimeValue size) {
        Player = player;
        Size = size;
    }

    public override void Run(GameController gameController) {
        GamePlayer player = Player.Evaluate() as GamePlayer;
        if (CheckTypeError(Player, player)) return;
        player.Hand.MaxHandSize = (int) Size.Evaluate();
    }
}