using System.Collections.Generic;

[System.Serializable]
public class Q_Players : Query {
    public override QueryResult Run(RunTimeValue target, GameController gameController) {
        QueryResult result = new QueryResult(gameController.GetPlayers());
        result.SetIsList(true);
        return result;
    }

    private static QueryData QueryData;
    public static QueryData GetQueryData () {
        if (QueryData == null) {
            QueryData = new QueryData() {
                name = "All players",
                fields = new List<FieldData>(),
                query = new Q_Players(),
                takesListOptions = true
            };
        }
        return QueryData;
    }
}