using System.Collections.Generic;

[System.Serializable]
public class Q_DeckSize : Query {
    public override QueryResult Run(RunTimeValue target, GameController gameController) {
        return new QueryResult(gameController.Deck.GetSize());
    }   

    private static QueryData QueryData;
    public static QueryData GetQueryData () {
        if (QueryData == null) {
            QueryData = new QueryData() {
                name = "Number of cards in deck",
                fields = new List<FieldData>(),
                query = new Q_DeckSize(),
                takesListOptions = false
            };
        }
        return QueryData;
    }
}