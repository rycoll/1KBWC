using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayer {

    public GamePlayer(string name)
    {
        Name = name;
        Hand = new Hand();
        Points = 0;
        Colour = Random.ColorHSV();
    }

    public string Name { get; set; }
    public Hand Hand { get; set; }
    public int Points { get; set; }
    public int DrawPerTurn { get; set; }
    public Color Colour { get; set; }

    public bool AddToHand (Card card)
    {
        return Hand.AddCard(card);
    }
}
