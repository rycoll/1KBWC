public class Q_Players : Query {
    public override QueryResult Run(object target, GameController gameController) {
        QueryResult result = new QueryResult(gameController.GetPlayers());
        result.SetIsList(true);
        return result;
    }
}