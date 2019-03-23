using System.Collections.Generic;

[System.Serializable]
public class Q_Opponents : Query<GamePlayer[]> {
    public override RunTimeValue<GamePlayer[]> Run(GameController gameController) {
        return new RunTimeValue<GamePlayer[]>(gameController.GetOpponents());
    }

    private static QueryData QueryData;
    public static QueryData GetQueryData () {
        if (QueryData == null) {
            QueryData = new QueryData() {
                name = "Opponents (all non-you players)",
                fields = new List<FieldData>(),
                takesListOptions = true
            };
        }
        return QueryData;
    }
}