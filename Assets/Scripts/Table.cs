using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Table : CardZone {
    private List<PlayedCard> playedCards;
    private GameController game;

    public Table(GameController game) {
        playedCards = new List<PlayedCard>();
        this.game = game;
    }

    public override bool AddCard(Card card) {
        return AddCard(game.ActivePlayerIndex, card);
    }

    public bool AddCard (int player, Card card) {
        playedCards.Add(new PlayedCard(player, card));
        return true;
    } 

    public override int GetSize () {
        return playedCards.Count;
    }

    public override bool RemoveCard (int id) {
        IEnumerable<PlayedCard> list = playedCards.Where(playedCard => playedCard.Card.id == id);
        PlayedCard card = list.ToArray()[0];
        if (card != null) {
            return playedCards.Remove(card);
        }
        Debug.LogError("Couldn't remove card: " + id);
        return false;
    }

    public override Card GetCard (int id) {
        IEnumerable<PlayedCard> list = playedCards.Where(playedCard => playedCard.Card.id == id);
        return list.ToArray()[0].Card;
    }

    public override Card[] GetCards () {
        return playedCards.Select(playedCard => playedCard.Card).ToArray();
    }

    public Card[] GetCardsByPlayer (int player) {
        IEnumerable<PlayedCard> playerCards = playedCards.Where(playedCard => playedCard.Owner == player);
        return playerCards.Select(playedCard => playedCard.Card).ToArray();
    }

    public Card[] GetCardsByPlayers (int[] players) {
        IEnumerable<PlayedCard> playerCards = playedCards.Where(playedCard => players.Contains(playedCard.Owner));
        return playerCards.Select(playedCard => playedCard.Card).ToArray();
    }
}
