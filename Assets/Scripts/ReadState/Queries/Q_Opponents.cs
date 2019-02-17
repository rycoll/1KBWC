using System.Collections.Generic;

[System.Serializable]
public class Q_Opponents : Query {
    public override QueryResult Run(RunTimeValue target, GameController gameController) {
        QueryResult result = new QueryResult(gameController.GetOpponents());
        result.SetIsList(true);
        return result;
    }

    private static QueryData QueryData;
    public static QueryData GetQueryData () {
        if (QueryData == null) {
            QueryData = new QueryData() {
                name = "Opponents (all non-you players)",
                fields = new List<FieldData>(),
                query = new Q_Opponents(),
                takesListOptions = true
            };
        }
        return QueryData;
    }
}