public class Q_PlayerHand : Query {
    public override QueryResult Run(object target, GameController gameController) {
        GamePlayer player = (GamePlayer) target;
        QueryResult result = new QueryResult(player.Hand.GetCards());
        result.SetIsList(true);
        return result;
    }
}