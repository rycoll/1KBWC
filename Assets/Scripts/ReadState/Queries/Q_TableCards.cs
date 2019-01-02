public class Q_TableCards : Query {
    public override QueryResult Run(object target, GameController gameController){ 
        QueryResult result = new QueryResult(gameController.Table.GetCards());
        result.SetIsList(true);
        return result;
    }
}