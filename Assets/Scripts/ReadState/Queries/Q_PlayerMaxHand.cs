using System.Collections.Generic;

[System.Serializable]
public class Q_PlayerMaxHand : Query {
    public override QueryResult Run(RunTimeValue target, GameController gameController) {
        GamePlayer player = (GamePlayer) target.Evaluate();
        return new QueryResult(player.Hand.MaxHandSize);
    }

    public static QueryData QueryData = new QueryData() {
        name = "A player's maximum hand size",
        fields = new List<FieldData>(){
            FieldLibrary.PlayerFieldData
        },
        query = new Q_PlayerMaxHand(),
        takesListOptions = false
    };
}