using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Hand : CardZone {

    public int MaxHandSize { get; private set; }
    private int OwnerIndex;

    public Hand(int playerIndex) : base() {
        OwnerIndex = playerIndex;
        MaxHandSize = 10;
    }

    public int GetOwner() {
        return OwnerIndex;
    }

    public override bool AddCard (Card cardToAdd)
    {
        if (cards.Count >= MaxHandSize)
        {
            return false;
        }
        cards.Add(cardToAdd);
        return true;
    }

    public void SetMax (int n) {
        MaxHandSize = n;
    } 
}
