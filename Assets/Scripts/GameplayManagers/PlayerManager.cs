using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerManager {

    private GamePlayer[] players;

    private int activePlayerIndex;
    public int GetActivePlayerIndex() => activePlayerIndex;

    public PlayerManager (int numPlayers) {
        players = new GamePlayer[numPlayers];
        for (int i = 0; i < numPlayers; i++) {
            players[i] = new GamePlayer($"Player {i + 1}", i);
        }
    }

    #region Accessors

    // need better error handling

    public int GetIndexOfPlayer(GamePlayer player) => Array.IndexOf(players, player);

    public GamePlayer GetActivePlayer() => players[activePlayerIndex];

    public GamePlayer GetLocalPlayer() => (GamePlayer)players.Where(player => IsLocalPlayer(player)).First();

    public GamePlayer GetPlayer(int index) => players[index];

    public GamePlayer[] GetPlayers() => players;

    // this will need to be updated
    private bool IsLocalPlayer(GamePlayer player) => player == players[0];

    public GamePlayer[] GetOpponents() => players.Where(player => !IsLocalPlayer(player)).ToArray();

    #endregion

    public Card FindCardById (int id) {
        foreach (GamePlayer player in players) {
            Card[] hand = player.Hand.GetCards().Where(card => card.GetID() == id).ToArray();
            if (hand.Length > 0) {
                return hand[0];
            }
        }
        return null;
    }

}