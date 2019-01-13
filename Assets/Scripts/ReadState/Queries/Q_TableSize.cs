[System.Serializable]
public class Q_TableSize : Query {
    public override QueryResult Run(RunTimeValue target, GameController gameController) {
        return new QueryResult(gameController.Table.GetSize());
    }
}