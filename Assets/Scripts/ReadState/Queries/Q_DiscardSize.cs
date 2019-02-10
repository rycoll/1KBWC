using System.Collections.Generic;

[System.Serializable]
public class Q_DiscardSize : Query {
    public override QueryResult Run(RunTimeValue target, GameController gameController) {
        return new QueryResult(gameController.Discard.GetSize());
    }

    public static QueryData QueryData = new QueryData() {
        name = "Number of cards in discard pile",
        fields = new List<FieldData>(),
        query = new Q_DiscardSize(),
        takesListOptions = false
    };

}