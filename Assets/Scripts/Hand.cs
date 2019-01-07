using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Hand {

    public int MaxHandSize { get; set; }
    private List<Card> cards;

    public Hand() {
        MaxHandSize = 10;
        cards = new List<Card>();
    }

    // return true if successful, false otherwise
    public bool AddCard (Card cardToAdd)
    {
        if (cards.Count >= MaxHandSize)
        {
            return false;
        }
        cards.Add(cardToAdd);
        return true;
    }

    public bool RemoveCard (Card cardToRemove) {
        return cards.Remove(cardToRemove);
    }

    public Card[] GetCards()
    {  
        // gives references to Card objects, which is technically insecure
        // pls don't exploit!
        return cards.ToArray();
    }

    public int GetNumCards() {
        return cards.Count;
    }
}
