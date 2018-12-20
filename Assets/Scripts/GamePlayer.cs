using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayer {

    private Hand hand;
    private int points;

    public GamePlayer()
    {
        hand = new Hand();
        points = 0;
    }

    public bool AddToHand (Card card)
    {
        return hand.AddCard(card);
    }
}
