using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayedCard
{
    public GamePlayer Owner { get; set; }
    public Card Card { get; set; }

    public PlayedCard (GamePlayer player, Card card) {
        Owner = player;
        Card = card;
    }
}
