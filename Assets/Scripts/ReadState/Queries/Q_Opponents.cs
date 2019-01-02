public class Q_Opponents : Query {
    public override QueryResult Run(object target, GameController gameController) {
        QueryResult result = new QueryResult(gameController.GetOpponents());
        result.SetIsList(true);
        return result;
    }
}