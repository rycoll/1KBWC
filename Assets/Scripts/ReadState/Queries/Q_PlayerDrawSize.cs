using System.Collections.Generic;

[System.Serializable]
public class Q_PlayerDrawSize : Query {
    public override QueryResult Run(RunTimeValue target, GameController gameController) {
        GamePlayer player = (GamePlayer) target.Evaluate();
        return new QueryResult(player.DrawPerTurn);
    }

    public static QueryData QueryData = new QueryData() {
        name = "Number of cards a player draws each turn",
        fields = new List<FieldData>() {
            FieldLibrary.PlayerFieldData
        },
        query = new Q_PlayerDrawSize(),
        takesListOptions = false
    };
}