using System.Collections.Generic;

[System.Serializable]
public class Q_ActivePlayer : Query<GamePlayer> {
    public override RunTimeValue<GamePlayer> Run(GameController gameController) {
        return new RunTimeValue<GamePlayer>(gameController.GetActivePlayer());
    }

    private static QueryData QueryData;
    public static QueryData GetQueryData () {
        if (QueryData == null) {
            QueryData = new QueryData() {
                name = "You (Player)",
                fields = new List<FieldData>(),
                takesListOptions = false
            };
        }
        return QueryData;
    }
}