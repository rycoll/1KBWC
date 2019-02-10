using System.Collections.Generic;

[System.Serializable]
public class Q_DiscardCards : Query {
    public override QueryResult Run(RunTimeValue target, GameController gameController) {
        QueryResult result = new QueryResult(gameController.Discard.GetCards());
        result.SetIsList(true);
        return result;
    }

    public static QueryData QueryData = new QueryData() {
        name = "Cards in discard pile",
        fields = new List<FieldData>(),
        query = new Q_DiscardCards(),
        takesListOptions = true
    };
}