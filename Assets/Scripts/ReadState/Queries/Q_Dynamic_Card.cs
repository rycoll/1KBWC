using System.Collections.Generic;

[System.Serializable]
public class Q_Dynamic_Card : Query<Card> {

    private int Lookup { get; set; }
    public Card DynamicCard { get; set; }

    public override RunTimeValue<Card> Run(GameController gameController) {
        // Card card = DynamicValueTable.GetCard(Lookup);
        // return new RunTimeValue<Card>(card);
        return new RunTimeValue<Card>(DynamicCard);
    }

    public Q_Dynamic_Card () {
        Lookup = DynamicValueTable.CardHash;
    }

    private static QueryData QueryData;
    public static QueryData GetQueryData () {
        if (QueryData == null) {
            QueryData = new QueryData() {
                name = "A value yet to be determined",
                fields = new List<FieldData>(),
                takesListOptions = false
            };
        }
        return QueryData;
    }
}