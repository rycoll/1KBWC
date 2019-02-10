using System.Collections.Generic;

[System.Serializable]
public class Q_PlayerHand : Query {
    public override QueryResult Run(RunTimeValue target, GameController gameController) {
        GamePlayer player = (GamePlayer) target.Evaluate();
        QueryResult result = new QueryResult(player.Hand.GetCards());
        result.SetIsList(true);
        return result;
    }

    public static QueryData QueryData = new QueryData() {
        name = "Cards in a player's hand",
        fields = new List<FieldData>(){
            FieldLibrary.PlayerFieldData
        },
        query = new Q_PlayerHand(),
        takesListOptions = true
    };
}