using System.Collections.Generic;

[System.Serializable]
public class Q_PlayerMaxHand : Query<int> {
    public RunTimeValue<GamePlayer> target;
    public override RunTimeValue<int> Run(GameController gameController) {
        GamePlayer player = target.Evaluate(gameController);
        return new RunTimeValue<int>(player.Hand.MaxHandSize);
    }

    private static QueryData QueryData;
    public static QueryData GetQueryData () {
        if (QueryData == null) {
            QueryData = new QueryData() {
                name = "A player's maximum hand size",
                fields = new List<FieldData>(){
                    FieldLibrary.GetPlayerFieldData()
                },
                takesListOptions = false
            };
        }
        return QueryData;
    }
}