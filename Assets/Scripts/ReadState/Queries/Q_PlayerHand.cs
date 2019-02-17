using System.Collections.Generic;

[System.Serializable]
public class Q_PlayerHand : Query {
    public override QueryResult Run(RunTimeValue target, GameController gameController) {
        GamePlayer player = (GamePlayer) target.Evaluate();
        QueryResult result = new QueryResult(player.Hand.GetCards());
        result.SetIsList(true);
        return result;
    }

    private static QueryData QueryData;
    public static QueryData GetQueryData () {
        if (QueryData == null) {
            QueryData = new QueryData() {
                name = "Cards in a player's hand",
                fields = new List<FieldData>(){
                    FieldLibrary.GetPlayerFieldData()
                },
                query = new Q_PlayerHand(),
                takesListOptions = true
            };
        }
        return QueryData;
    }
}