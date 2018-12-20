using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this needs to be networked or whatever
public class GameController : MonoBehaviour {

    private UIController UI;
    private Deck deck;
    private GamePlayer[] players;
    private int activePlayerIndex;

    private void Start() {
        UI = this.GetComponent<UIController>();
        deck = new Deck();
        players = new GamePlayer[] {new GamePlayer(), new GamePlayer()};
        activePlayerIndex = 0;
	}

    private GamePlayer GetActivePlayer ()
    {
        return players[activePlayerIndex];
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

    private bool isLocalPlayer(GamePlayer player) {
        // update this eventually, obviously
        return true;
    }

    private void GiveCardToPlayer(GamePlayer player, Card card)
    {
        if (player.AddToHand(card))
        {
            // card was successfully added, update UI accordingly
            // if it's the active player, add a card to their hand
            if (isLocalPlayer(player))
            {
                UI.AddCardToHandDisplay(card);
            }
            // if it's not, just increment the little hand-size counter for the appropriate player
        }
        else
        {
            // card could not be added, UI should give some feedback
        }
    }
	
    
}
