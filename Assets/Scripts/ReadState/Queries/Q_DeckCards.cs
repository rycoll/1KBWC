using System.Collections.Generic;

[System.Serializable]
public class Q_DeckCards : Query {
    public override QueryResult Run(RunTimeValue target, GameController gameController){
        QueryResult result = new QueryResult(gameController.Deck.GetCards());
        result.SetIsList(true);
        return result;
    }   

    private static QueryData QueryData;
    public static QueryData GetQueryData () {
        if (QueryData == null) {
            QueryData = new QueryData() {
                name = "Cards in deck",
                fields = new List<FieldData>(),
                query = new Q_DeckCards(),
                takesListOptions = true
            };
        }
        return QueryData;
    }

}