using System.Collections.Generic;

[System.Serializable]
public class Q_TableSize : Query {
    public override QueryResult Run(RunTimeValue target, GameController gameController) {
        return new QueryResult(gameController.Table.GetSize());
    }

    public static QueryData QueryData = new QueryData() {
        name = "Number of cards in play",
        fields = new List<FieldData>(),
        query = new Q_TableSize(),
        takesListOptions = false
    };
}