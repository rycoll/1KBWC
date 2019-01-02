public class Q_Flag : Query {
    public override QueryResult Run(object target, GameController gameController){
        string key = (string) target;
        return new QueryResult(gameController.Variables.IsFlag(key));
    }
}