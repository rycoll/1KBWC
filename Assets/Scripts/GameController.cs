using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// this needs to be networked or whatever
public class GameController : MonoBehaviour {

    private UIController UI;
    public Deck Deck { get; set;}
    public Deck Discard { get; set; }
    public Table Table { get; set; }
    public GameVariables Variables { get; set; }
    public GameListeners Listeners { get; set; }
    private GamePlayer[] players;
    private int activePlayerIndex;

    private void Start() {
        UI = this.GetComponent<UIController>();
        Deck = new Deck();
        Table = new Table();
        Variables = new GameVariables();
        Listeners = new GameListeners();
        players = new GamePlayer[] {
            new GamePlayer("Player1"), 
            new GamePlayer("Player2"),
            new GamePlayer("Player3"),
            new GamePlayer("Player4")
        };
        activePlayerIndex = 0;

        GiveCardToPlayer(players[0], Deck.Pop());
        GiveCardToPlayer(players[1], Deck.Pop());
        GiveCardToPlayer(players[2], Deck.Pop());
        GiveCardToPlayer(players[2], Deck.Pop());
        GiveCardToPlayer(players[2], Deck.Pop());
        GiveCardToPlayer(players[3], Deck.Pop());

        UI.RefreshOpponentDisplay(this.GetOpponents());
	}

    public GamePlayer GetActivePlayer ()
    {
        return players[activePlayerIndex];
    }

    public GamePlayer[] GetPlayers () {
        return players;
    }

    private bool IsLocalPlayer(GamePlayer player) {
        // update this eventually, obviously
        return player == players[0];
    }

    public GamePlayer[] GetOpponents () {
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
        GiveCardToPlayer(GetActivePlayer(), Deck.Pop());
        UI.SetDeckText(Deck.GetSize());
    }

    public void GiveCardToPlayer(GamePlayer player, Card card)
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
