public class Q_DeckSize : Query {
    public override QueryResult Run(RunTimeValue target, GameController gameController) {
        return new QueryResult(gameController.Deck.GetSize());
    }
}