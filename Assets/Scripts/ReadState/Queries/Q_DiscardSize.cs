using System.Collections.Generic;

[System.Serializable]
public class Q_DiscardSize : Query<int> {
    public override RunTimeValue<int> Run(GameController gameController) {
        return new RunTimeValue<int>(gameController.Discard.GetSize());
    }

    private static QueryData QueryData;
    public static QueryData GetQueryData () {
        if (QueryData == null) {
            QueryData = new QueryData() {
                name = "Number of cards in discard pile",
                fields = new List<FieldData>(),
                takesListOptions = false
            };
        }
        return QueryData;
    }
}