using System.Collections.Generic;

[System.Serializable]
public class Q_TableCards : Query {
    public override QueryResult Run(RunTimeValue target, GameController gameController){ 
        QueryResult result = new QueryResult(gameController.Table.GetCards());
        result.SetIsList(true);
        return result;
    }

    private static QueryData QueryData;
    public static QueryData GetQueryData () {
        if (QueryData == null) {
            QueryData = new QueryData() {
                name = "Cards in play",
                fields = new List<FieldData>(),
                query = new Q_TableCards(),
                takesListOptions = true
            };
        }
        return QueryData;
    }
}