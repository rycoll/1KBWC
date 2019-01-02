public class Q_DeckCards : Query {
    public override QueryResult Run(object target, GameController gameController){
        QueryResult result = new QueryResult(gameController.Deck.GetCards());
        result.SetIsList(true);
        return result;
    }
}