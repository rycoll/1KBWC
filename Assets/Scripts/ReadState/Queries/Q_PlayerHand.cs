using System.Collections.Generic;

[System.Serializable]
public class Q_PlayerHand : Query<Card[]> {
    public RunTimeValue<GamePlayer> target;
    public override RunTimeValue<Card[]> Run(GameController gameController) {
        GamePlayer player = target.Evaluate(gameController);
        return new RunTimeValue<Card[]>(player.Hand.GetCards());
    }

    private static QueryData QueryData;
    public static QueryData GetQueryData () {
        if (QueryData == null) {
            QueryData = new QueryData() {
                name = "Cards in a player's hand",
                fields = new List<FieldData>(){
                    FieldLibrary.GetPlayerFieldData()
                },
                takesListOptions = true
            };
        }
        return QueryData;
    }
}