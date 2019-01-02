public class Q_PlayerMaxHand : Query {
    public override QueryResult Run(object target, GameController gameController) {
        GamePlayer player = (GamePlayer) target;
        return new QueryResult(player.Hand.MaxHandSize);
    }
}