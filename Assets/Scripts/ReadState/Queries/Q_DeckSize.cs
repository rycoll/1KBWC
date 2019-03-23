using System.Collections.Generic;

[System.Serializable]
public class Q_DeckSize : Query<int> {
    public override RunTimeValue<int> Run(GameController gameController) {
        return new RunTimeValue<int>(gameController.Deck.GetSize());
    }   

    private static QueryData QueryData;
    public static QueryData GetQueryData () {
        if (QueryData == null) {
            QueryData = new QueryData() {
                name = "Number of cards in deck",
                fields = new List<FieldData>(),
                takesListOptions = false
            };
        }
        return QueryData;
    }
}