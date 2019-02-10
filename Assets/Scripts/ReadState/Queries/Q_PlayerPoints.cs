using System.Collections.Generic;

[System.Serializable]
public class Q_PlayerPoints : Query {
    public override QueryResult Run(RunTimeValue target, GameController gameController) {
        GamePlayer player = (GamePlayer) target.Evaluate();
        return new QueryResult(player.Points);
    }

    public static QueryData QueryData = new QueryData() {
        name = "A player's score",
        fields = new List<FieldData>(){
            FieldLibrary.PlayerFieldData
        },
        query = new Q_PlayerPoints(),
        takesListOptions = false
    };
}