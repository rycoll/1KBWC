using System.Collections.Generic;

[System.Serializable]
public class Q_DiscardCards : Query<Card[]> {
    public override RunTimeValue<Card[]> Run(GameController gameController) {
        return new RunTimeValue<Card[]>(gameController.Discard.GetCards());
    }

    private static QueryData QueryData;
    public static QueryData GetQueryData () {
        if (QueryData == null) {
            QueryData = new QueryData() {
                name = "Cards in discard pile",
                fields = new List<FieldData>(),
                takesListOptions = true
            };
        }
        return QueryData;
    }
}