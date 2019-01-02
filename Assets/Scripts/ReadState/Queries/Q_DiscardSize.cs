public class Q_DiscardSize : Query {
    public override QueryResult Run(object target, GameController gameController) {
        return new QueryResult(gameController.Discard.GetSize());
    }
}