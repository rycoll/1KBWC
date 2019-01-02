public class Q_PlayerName : Query {
    public override QueryResult Run(object target, GameController gameController) {
        GamePlayer player = (GamePlayer) target;
        return new QueryResult(player.Name);
    }
}