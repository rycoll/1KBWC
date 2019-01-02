public class Q_TableSize : Query {
    public override QueryResult Run(object target, GameController gameController) {
        return new QueryResult(gameController.Table.GetSize());
    }
}