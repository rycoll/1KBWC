[System.Serializable]
public class Q_Opponents : Query {
    public override QueryResult Run(RunTimeValue target, GameController gameController) {
        QueryResult result = new QueryResult(gameController.GetOpponents());
        result.SetIsList(true);
        return result;
    }
}