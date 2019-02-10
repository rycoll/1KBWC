using System.Collections.Generic;

[System.Serializable]
public class Q_PlayerName : Query {
    public override QueryResult Run(RunTimeValue target, GameController gameController) {
        GamePlayer player = (GamePlayer) target.Evaluate();
        return new QueryResult(player.Name);
    }

    public static QueryData QueryData = new QueryData() {
        name = "A player's name",
        fields = new List<FieldData>(){
            FieldLibrary.PlayerFieldData
        },
        query = new Q_PlayerName(),
        takesListOptions = false
    };
}