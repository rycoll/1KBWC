public class Q_DiscardSize : Query {
    public override QueryResult Run(RunTimeValue target, GameController gameController) {
        return new QueryResult(gameController.Discard.GetSize());
    }
}