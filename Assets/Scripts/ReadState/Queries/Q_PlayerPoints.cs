using System.Collections.Generic;

[System.Serializable]
public class Q_PlayerPoints : Query<int> {
    public RunTimeValue<GamePlayer> target;
    public override RunTimeValue<int> Run(GameController gameController) {
        GamePlayer player = target.Evaluate(gameController);
        return new RunTimeValue<int>(player.Points);
    }

    private static QueryData QueryData;
    public static QueryData GetQueryData () {
        if (QueryData == null) {
            QueryData = new QueryData() {
                name = "A player's score",
                fields = new List<FieldData>(){
                    FieldLibrary.GetPlayerFieldData()
                },
                takesListOptions = false
            };
        }
        return QueryData;
    }
}