using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayer {

    public GamePlayer(string name, int index)
    {
        Name = name;
        Hand = new Hand(index);
        Index = index;
        Points = 0;
        DrawPerTurn = 1;
        Colour = Random.ColorHSV();
        // set default win condition
        byte[] indexBytes = LiteralFactory.CreateIntLiteral(index);
        byte[] getPlayerPoints = InstructionFactory.Make_GetPlayerPoints(indexBytes);
        byte[] numberToCompare = LiteralFactory.CreateIntLiteral(100);
        WinCondition = LiteralFactory.CreateConditionLiteral(getPlayerPoints, numberToCompare, ConditionType.NUM, ConditionOperator.AT_LEAST);
    }

    public string Name { get; set; }
    public Hand Hand { get; set; }
    public int Index { get; set; }
    public int Points { get; set; }
    public int DrawPerTurn { get; private set; }
    public Color Colour { get; set; }
    public byte[] WinCondition { get; set; }

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
