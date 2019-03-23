using System.Collections.Generic;

[System.Serializable]
public class Q_Players : Query<GamePlayer[]> {
    public override RunTimeValue<GamePlayer[]> Run(GameController gameController) {
        return new RunTimeValue<GamePlayer[]>(gameController.GetPlayers());
    }

    private static QueryData QueryData;
    public static QueryData GetQueryData () {
        if (QueryData == null) {
            QueryData = new QueryData() {
                name = "All players",
                fields = new List<FieldData>(),
                takesListOptions = true
            };
        }
        return QueryData;
    }
}