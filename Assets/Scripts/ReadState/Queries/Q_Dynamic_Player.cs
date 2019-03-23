using System.Collections.Generic;

[System.Serializable]
public class Q_Dynamic_Player : Query<GamePlayer> {

    // private int Lookup { get; set; }
    public GamePlayer Player { get; set; }

    public override RunTimeValue<GamePlayer> Run(GameController gameController) {
        // GamePlayer player = DynamicValueTable.GetPlayer(Lookup);
        // return new RunTimeValue<GamePlayer>(player);
        return new RunTimeValue<GamePlayer>(Player);
    }

    public Q_Dynamic_Player () {
        //Lookup = DynamicValueTable.CardHash;
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