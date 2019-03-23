using System.Collections.Generic;

[System.Serializable]
public class Q_PlayerDrawSize : Query<int> {
    public RunTimeValue<GamePlayer> target;
    public override RunTimeValue<int> Run(GameController gameController) {
        GamePlayer player = target.Evaluate(gameController);
        return new RunTimeValue<int>(player.DrawPerTurn);
    }   

    private static QueryData QueryData;
    public static QueryData GetQueryData () {
        if (QueryData == null) {
            QueryData = new QueryData() {
                name = "Number of cards a player draws each turn",
                fields = new List<FieldData>() {
                    FieldLibrary.GetPlayerFieldData()
                },
                takesListOptions = false
            };
        }
        return QueryData;
    }
}