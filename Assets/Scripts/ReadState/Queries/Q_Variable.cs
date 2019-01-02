public class Q_Variable : Query {
    public override QueryResult Run(object target, GameController gameController) {
        string key = (string) target;
        return new QueryResult(gameController.Variables.GetVariable(key));
    }
}