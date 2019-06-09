public class GameFunctions {

    public void PlayerDrawCard(GamePlayer player, Deck deck) {
        GiveCardToPlayer(player, deck.Pop());
    }

    public void GiveCardToPlayer(GamePlayer player, Card card) {
        player.AddToHand(card);
    }

    public void SetPlayerPoints(GamePlayer player, int points) {
        player.Points = points;
    }
}