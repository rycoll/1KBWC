using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Hand {

    private int maxHandSize;
    private List<Card> cards;

    public Hand() {
        maxHandSize = 10;
        cards = new List<Card>();
    }

    // return true if successful, false otherwise
    public bool AddCard (Card cardToAdd)
    {
        if (cards.Count >= maxHandSize)
        {
            return false;
        }
        cards.Add(cardToAdd);
        return true;
    }

    public Card[] GetCards()
    {  
        // gives references to Card objects, which is technically insecure
        // pls don't exploit!
        return cards.ToArray();
    }
}
