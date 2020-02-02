using System.Linq;

public class CardManager {
    public Deck Deck { get; set;}
    public Deck Discard { get; set; }
    public Table Table { get; set; }

    public CardManager () {
        Deck = new Deck();
        Discard = new Deck();
        Table = new Table();
    }

    public Card FindCardById (string id) {
        Card[] cards = Table.GetCards().Where(card => card.GetID() == id).ToArray();
        if (cards.Length > 0) {
            return cards[0];
        }
        Card[] deck = Deck.GetCards().Where(card => card.GetID() == id).ToArray();
        if (deck.Length > 0) {
            return deck[0];
        }
        Card[] discard = Discard.GetCards().Where(card => card.GetID() == id).ToArray();
        if (discard.Length > 0) {
            return discard[0];
        }
        return null;
    }
}