using System.Collections.Generic;

[System.Serializable]
public class Q_TableSize : Query<int> {
    public override RunTimeValue<int> Run(GameController gameController) {
        return new RunTimeValue<int>(gameController.Table.GetSize());
    }

    private static QueryData QueryData;
    public static QueryData GetQueryData () {
        if (QueryData == null) {
            QueryData = new QueryData() {
                name = "Number of cards in play",
                fields = new List<FieldData>(),
                takesListOptions = false
            };
        }
        return QueryData;
    }
}