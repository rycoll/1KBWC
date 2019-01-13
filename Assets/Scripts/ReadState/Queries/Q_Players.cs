[System.Serializable]
public class Q_Players : Query {
    public override QueryResult Run(RunTimeValue target, GameController gameController) {
        QueryResult result = new QueryResult(gameController.GetPlayers());
        result.SetIsList(true);
        return result;
    }
}