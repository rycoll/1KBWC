[System.Serializable]
public class Q_Variable : Query {
    public override QueryResult Run(RunTimeValue target, GameController gameController) {
        string key = (string) target.Evaluate();
        return new QueryResult(gameController.Variables.GetVariable(key));
    }
}