using System.Collections.Generic;

[System.Serializable]
public class Q_PlayerName : Query<string> {
    public RunTimeValue<GamePlayer> target;
    public override RunTimeValue<string> Run(GameController gameController) {
        GamePlayer player = target.Evaluate(gameController);
        return new RunTimeValue<string>(player.Name);
    }

    private static QueryData QueryData;
    public static QueryData GetQueryData () {
        if (QueryData == null) {
            QueryData = new QueryData() {
                name = "A player's name",
                fields = new List<FieldData>(){
                    FieldLibrary.GetPlayerFieldData()
                },
                takesListOptions = false
            };
        }
        return QueryData;
    }
}