public class TestCardManager : CardManager {
    public TestCardManager(int deckSize, int discardSize, int tableSize) {
        Deck = new Deck();
        for (int i = 0; i < deckSize; i++) {
            Deck.AddCard(new TestCard(), DeckLocation.TOP);
        }

        Discard = new Deck();
        for (int i = 0; i < discardSize; i++) {
            Discard.AddCard(new TestCard(), DeckLocation.TOP);
        }

        Table = new Table();
        for (int i = 0; i < tableSize; i++) {
            Table.AddCard(new TestCard());
        }

    }
}