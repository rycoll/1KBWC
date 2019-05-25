using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayer {

    public GamePlayer(string name, int index)
    {
        Name = name;
        Hand = new Hand(index);
        Points = 0;
        DrawPerTurn = 1;
        Colour = Random.ColorHSV();
        // set default win condition
        Q_PlayerPoints pointQuery = new Q_PlayerPoints();
        pointQuery.target = new RunTimeValue<GamePlayer>(this);
        RunTimeValue<int> playerPointQuery = new RunTimeValue<int>(
            new QueryRequest<int>(pointQuery)    
        );
        RunTimeValue<int> winThreshold = new RunTimeValue<int>(100);
        WinCondition = new Condition(playerPointQuery, winThreshold, ConditionOperator.AT_LEAST);
    }

    public string Name { get; set; }
    public Hand Hand { get; set; }
    public int Points { get; set; }
    public int DrawPerTurn { get; private set; }
    public Color Colour { get; set; }
    public Condition WinCondition { get; set; }

    public bool AddToHand (Card card)
    {
        return Hand.AddCard(card);
    }

    public void SetDrawPerTurn (int n) {
        if (n >= 1) {
            this.DrawPerTurn = n;
        }
    }
}
