using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// this needs to be networked or whatever
public class GameController : MonoBehaviour {

    private UIController UI;
    private Deck deck;
    private GamePlayer[] players;
    private int activePlayerIndex;

    private void Start() {
        UI = this.GetComponent<UIController>();
        deck = new Deck();
        players = new GamePlayer[] {
            new GamePlayer("Player1"), 
            new GamePlayer("Player2"),
            new GamePlayer("Player3"),
            new GamePlayer("Player4")
        };
        activePlayerIndex = 0;

        GiveCardToPlayer(players[0], deck.Pop());
        GiveCardToPlayer(players[1], deck.Pop());
        GiveCardToPlayer(players[2], deck.Pop());
        GiveCardToPlayer(players[2], deck.Pop());
        GiveCardToPlayer(players[2], deck.Pop());
        GiveCardToPlayer(players[3], deck.Pop());

        UI.RefreshOpponentDisplay(this.GetOpponents());
	}

    private GamePlayer GetActivePlayer ()
    {
        return players[activePlayerIndex];
    }

    private bool IsLocalPlayer(GamePlayer player) {
        // update this eventually, obviously
        return player == players[0];
    }

    private GamePlayer[] GetOpponents () {
        return players.Where(player => !IsLocalPlayer(player)).ToArray();
    }

    public void PassTurn()
    {
        if (++activePlayerIndex >= players.Length)
        {
            activePlayerIndex = 0;
        }
        DrawPhase();
    }

    public void DrawPhase()
    {
        GiveCardToPlayer(GetActivePlayer(), deck.Pop());
    }

    private void GiveCardToPlayer(GamePlayer player, Card card)
    {
        if (player.AddToHand(card))
        {
            if (IsLocalPlayer(player))
            {
                UI.AddCardToHandDisplay(card);
            } else {
                // change display for opponent
            }
        } else {
            // card could not be added, UI should give some feedback
        }
    }  
}
