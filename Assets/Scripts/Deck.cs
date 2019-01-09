using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck {

    private List<Card> cards = new List<Card>();

    public Deck ()
    {
        cards = new List<Card>();
    }

    // return the top card and remove it from the deck
    public Card Pop ()
    {
        // at the moment, this will just return an example card
        return new PlaceholderCard();
    }

    public int GetSize () {
        return cards.Count;
    }

    public Card[] GetCards () {
        // this probably needs to return a copy!
        return cards.ToArray();
    }

    public void Shuffle ()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            Card cardToShuffle = cards[i];
            cards.RemoveAt(i);
            cards.Insert(Random.Range(0, cards.Count), cardToShuffle);
        }
    }

    public void AddCard (Card card, DECK_LOCATION location) {
        switch (location) {
            case DECK_LOCATION.BOTTOM:
                cards.Add(card);
                break;
            case DECK_LOCATION.TOP:
                cards.Insert(0, card);
                break;
            case DECK_LOCATION.RANDOM:
                cards.Insert(Random.Range(0, GetSize() - 1), card);
                break;
            default:
                cards.Add(card);
                break;
        }
    }
}

public enum DECK_LOCATION {
    TOP, BOTTOM, RANDOM
}
