using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayedCard
{
    public int Owner { get; set; }
    public Card Card { get; set; }

    public PlayedCard (int player, Card card) {
        Owner = player;
        Card = card;
    }
}
