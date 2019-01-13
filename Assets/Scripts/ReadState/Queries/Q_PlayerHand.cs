[System.Serializable]
public class Q_PlayerHand : Query {
    public override QueryResult Run(RunTimeValue target, GameController gameController) {
        GamePlayer player = (GamePlayer) target.Evaluate();
        QueryResult result = new QueryResult(player.Hand.GetCards());
        result.SetIsList(true);
        return result;
    }
}