using System.Collections.Generic;

[System.Serializable]
public class Q_ActivePlayer : Query {
    public override QueryResult Run(RunTimeValue target, GameController gameController) {
        return new QueryResult(gameController.GetActivePlayer());
    }

    private static QueryData QueryData;
    public static QueryData GetQueryData () {
        if (QueryData == null) {
            QueryData = new QueryData() {
                name = "You (Player)",
                fields = new List<FieldData>(),
                query = new Q_ActivePlayer(),
                takesListOptions = false
            };
        }
        return QueryData;
    }
}