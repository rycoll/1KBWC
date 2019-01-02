public class Q_Counter : Query {
    public override QueryResult Run(object target, GameController gameController) {
        string key = (string) target;
        return new QueryResult(gameController.Variables.GetCounter(key));
    }
}