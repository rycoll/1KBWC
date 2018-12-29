using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Table
{
    private List<PlayedCard> cards;

    public Table() {
        cards = new List<PlayedCard>();
    }

    public void AddCard (GamePlayer player, Card card) {
        cards.Add(new PlayedCard(player, card));
    }

    public int GetSize () {
        return cards.Count;
    }

    public Card[] GetCards () {
        return cards.Select(playedCard => playedCard.Card).ToArray();
    }

    public Card[] GetCardsByPlayer (GamePlayer player) {
        IEnumerable<PlayedCard> playedCards = cards.Where(playedCard => playedCard.Owner == player);
        return playedCards.Select(playedCard => playedCard.Card).ToArray();
    }

    public Card[] GetCardsByPlayers (GamePlayer[] players) {
        IEnumerable<PlayedCard> playedCards = cards.Where(playedCard => players.Contains(playedCard.Owner));
        return playedCards.Select(playedCard => playedCard.Card).ToArray();
    }
}
