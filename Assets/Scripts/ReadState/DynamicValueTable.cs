using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DynamicValueTable {

    private static Dictionary<int, GamePlayer> playerLookup = new Dictionary<int, GamePlayer>();
    private static Dictionary<int, Card> cardLookup = new Dictionary<int, Card>();

    private static int playerHash = 0;
    private static int cardHash = 0;

    public static int PlayerHash {
        get {return ++playerHash;}
    }
    public static int CardHash {
        get {return ++cardHash;}
    }

    public static int AddCard (Card card, int index = -1) {
        if (index == -1) index = ++cardHash;
        cardLookup.Add(index, card);
        return cardHash;
    }

    public static int AddPlayer (GamePlayer player, int index = -1) {
        if (index == -1) index = ++playerHash;
        playerLookup.Add(index, player);
        return playerHash;
    }

    public static Card GetCard (int lookup) {
        Card card = cardLookup[lookup];
        cardLookup.Remove(lookup);
        return card;
    }

    public static GamePlayer GetPlayer (int lookup) {
        GamePlayer player = playerLookup[lookup];
        playerLookup.Remove(lookup);
        return player;
    }

}