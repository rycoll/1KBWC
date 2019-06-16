using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardZone {
    protected List<Card> cards = new List<Card>();

    public CardZone () {
        cards = new List<Card>();
    }

    public virtual int GetSize () {
        return cards.Count;
    }

    public virtual Card[] GetCards () {
        // this probably needs to return a copy!
        return cards.ToArray();
    }

    public virtual Card GetCard (int id) {
        foreach (Card card in cards) {
            if (card.GetID() == id) {
                return card;
            }
        }
        return null;
    }

    public virtual bool AddCard (Card card) {
        cards.Add(card);
        return true;
    }

    public virtual bool RemoveCard (int id) {
        Card card = GetCard(id);
        if (card != null) {
            return cards.Remove(card);
        }
        Debug.LogError("Couldn't remove card: " + id);
        return false;
    }

    public virtual int GetNumCards() {
        return cards.Count;
    }

    public bool MoveCard (CardZone dest, int id) {
        Card card = GetCard(id);
        if (card == null || !RemoveCard(id)) {
            return false;
        }
        return dest.AddCard(card);
    }
}