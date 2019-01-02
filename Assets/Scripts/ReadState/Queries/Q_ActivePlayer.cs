public class Q_ActivePlayer : Query {
    public override QueryResult Run(object target, GameController gameController) {
        return new QueryResult(gameController.GetActivePlayer());
    }
}