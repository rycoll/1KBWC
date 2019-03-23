using System.Collections.Generic;

[System.Serializable]
public class Q_DeckRandomCard : Query<Card> {
    public override RunTimeValue<Card> Run(GameController gameController) {
        return new RunTimeValue<Card>(gameController.Deck.GetCard(DECK_LOCATION.RANDOM));
    }

    private static QueryData QueryData;
    public static QueryData GetQueryData () {
        if (QueryData == null) {
            QueryData = new QueryData() {
                name = "Random card in deck",
                fields = new List<FieldData>() {},
                takesListOptions = false
            };
        }
        return QueryData;
    }
}