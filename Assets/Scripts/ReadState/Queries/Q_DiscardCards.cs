public class Q_DiscardCards : Query {
    public override QueryResult Run(RunTimeValue target, GameController gameController) {
        QueryResult result = new QueryResult(gameController.Discard.GetCards());
        result.SetIsList(true);
        return result;
    }
}