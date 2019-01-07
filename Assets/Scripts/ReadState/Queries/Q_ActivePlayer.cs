public class Q_ActivePlayer : Query {
    public override QueryResult Run(RunTimeValue target, GameController gameController) {
        return new QueryResult(gameController.GetActivePlayer());
    }
}