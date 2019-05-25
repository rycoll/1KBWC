using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : CardZone {

    // return the top card and remove it from the deck
    public Card Pop ()
    {
        if (GetSize() == 0) {
            return null;
        } else {
            Card draw = cards[0];
            cards.RemoveAt(0);
            return draw;
        }
    }

    public Card GetCard (DeckLocation location) {
        switch (location) {
            case DeckLocation.BOTTOM:
                return cards[cards.Count - 1];
            case DeckLocation.RANDOM:
                return cards[Random.Range(0, cards.Count - 1)];
            case DeckLocation.TOP:
                return cards[0];
            default:
                return null;
        }
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

    public void AddCard (Card card, DeckLocation location) {
        switch (location) {
            case DeckLocation.BOTTOM:
                cards.Add(card);
                break;
            case DeckLocation.TOP:
                cards.Insert(0, card);
                break;
            case DeckLocation.RANDOM:
                cards.Insert(Random.Range(0, GetSize() - 1), card);
                break;
            case DeckLocation.SHUFFLE:
                cards.Add(card);
                Shuffle();
                break;
            default:
                cards.Add(card);
                break;
        }
    }

    public void MoveLastAddedCard (DeckLocation location) {
        Card card = cards[GetSize() - 1];
        cards.RemoveAt(GetSize() - 1);
        AddCard(card, location);
    } 
}

public enum DeckLocation {
    TOP, BOTTOM, RANDOM, SHUFFLE
}
