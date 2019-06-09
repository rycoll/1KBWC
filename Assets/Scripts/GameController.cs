using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// this needs to be networked or whatever
public class GameController : MonoBehaviour {
    protected UIController UI;
    protected Interpreter Interpreter;
    public Deck Deck { get; set;}
    public Deck Discard { get; set; }
    public Table Table { get; set; }
    public GameVariables Variables { get; set; }
    public GameListeners Listeners { get; set; }
    protected GamePlayer[] players;
    protected int activePlayerIndex;
    public int ActivePlayerIndex {
        get { return activePlayerIndex; }
    }

    protected void Start() {
        UI = this.GetComponent<UIController>();
        Interpreter = new Interpreter(this);
        Deck = new Deck();
        Discard = new Deck();
        Table = new Table();
        Variables = new GameVariables();
        Listeners = new GameListeners();
        players = new GamePlayer[] {
            new GamePlayer("Player1", 0), 
            new GamePlayer("Player2", 1),
            new GamePlayer("Player3", 2)
        };
        activePlayerIndex = 0;

        UI.RefreshOpponentDisplay(this.GetOpponents());
        UI.DisplayOpponentCards(players[1]);
	}

    public int GetIndexOfPlayer (GamePlayer player) {
        return Array.IndexOf(players, player);
    }

    public GamePlayer GetActivePlayer ()
    {
        return players[activePlayerIndex];
    }

    public GamePlayer GetLocalPlayer () {
        return (GamePlayer) players.Where(player => IsLocalPlayer(player)).First();
    }

    public GamePlayer GetPlayer(int index) {
        // need better error handling here
        return players[index];
    }

    public GamePlayer[] GetPlayers () {
        return players;
    }

    protected bool IsLocalPlayer(GamePlayer player) {
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
        if (IsLocalPlayer(GetActivePlayer())) {
            // indicate that it's your turn
        } else {
            // indicate whose turn it is

            // for now, we'll just have NPCs play the first card in their hand
            GamePlayer active = GetActivePlayer();
            if (active.Hand.GetNumCards() > 0) {
                PlayCard(active, active.Hand.GetCards()[0]);
            } else {
                PassTurn();
            }
        }
    }

    public void DrawPhase()
    {
        PlayerDrawCard(GetActivePlayer());
        UI.SetDeckText(Deck.GetSize());
    }

    public void PlayerDrawCard(GamePlayer player) {
        GiveCardToPlayer(player, Deck.Pop());
    }

    public void GiveCardToPlayer(GamePlayer player, Card card)
    {
        if (player.AddToHand(card))
        {
            if (IsLocalPlayer(player))
            {
                UI.AddCardToHandDisplay(card);
            } else {
                UI.RefreshOpponentDisplay(GetOpponents());
            }
        } else {
            // card could not be added, UI should give some feedback
        }
    }

    public void PlayCard(GamePlayer player, Card card) {
        player.Hand.RemoveCard(card.id);
        // clone card effects instead??
        ExecuteEffects(card.Effects);
        Discard.AddCard(card, DeckLocation.TOP);
        // rack up animations etc asynchronously, play them, THEN continue on
    }

    public Card FindCardById (int id) {
        Card[] cards = Table.GetCards().Where(card => card.id == id).ToArray();
        if (cards.Length > 0) {
            return cards[0];
        }
        foreach (GamePlayer player in players) {
            Card[] hand = player.Hand.GetCards().Where(card => card.id == id).ToArray();
            if (hand.Length > 0) {
                return cards[0];
            }
        }
        Card[] deck = Deck.GetCards().Where(card => card.id == id).ToArray();
        if (deck.Length > 0) {
            return deck[0];
        }
        Card[] discard = Discard.GetCards().Where(card => card.id == id).ToArray();
        if (discard.Length > 0) {
            return discard[0];
        }
        return null;
    }
    
    public bool EvaluatePlayerWinCondition (GamePlayer player) {
        AddToStack(player.WinCondition);
        Condition condition = Interpreter.ReadConditionLiteral();
        return condition.Evaluate();
    }
    public bool CheckForWinner () {
        foreach (GamePlayer player in players) {
            if (EvaluatePlayerWinCondition(player)) {
                Debug.Log(player.Name + " wins!");
                return true;
            }
        }
        return false;
    }

    public void AddToDeck(Card card, DeckLocation loc) {
        Deck.AddCard(card, loc);
        UI.SetDeckText(Deck.GetSize());
    }

    public void AddToDiscard(Card card, DeckLocation loc) {
        Discard.AddCard(card, loc);
        UI.AddToDiscardDisplay(card);
        UI.SetDiscardText(Discard.GetSize());
    }

    public void ExecuteEffects (List<byte> effects) {
        AddToStack(effects.ToArray());
        while (Interpreter.GetCurrentStackSize() > 0) {
            Interpreter.next();
            // pause between effects here
        }
    }

    public void AddToStack(byte[] bytes) {
        Interpreter.push(bytes);
    }

    public void SetFlag (string flag, bool add) {
        Variables.SetFlag(flag, add);
        UI.UpdateFlagsText();
    }
    public void SetVariable (string key, string value) {
        Variables.SetVariable(key, value);
        UI.UpdateFlagsText();
    }
    public void SetCounter (string key, int value) {
        Variables.SetCounter(key, value);
        UI.UpdateFlagsText();
    }

    public void SetPlayerPoints (GamePlayer player, int points) {
        player.Points = points;
        UI.UpdatePointDisplays(GetLocalPlayer(), GetOpponents());
    }

    public void PresentChoiceOfCards (List<Card> choiceSet) {
        if (choiceSet.Count == 0) {
            Interpreter.CardChoiceCallback(null);
        }
        // currently always presents to active player
        if (GetActivePlayer() == GetLocalPlayer()) {
            UI.PresentChoiceOfCards(choiceSet, Interpreter);
        }
    }

    public void PresentChoiceOfPlayers (List<GamePlayer> choiceSet) {
        if (choiceSet.Count == 0) {
            Interpreter.PlayerChoiceCallback(null);
        }
        
        // currently always presents to active player
        if (GetActivePlayer() == GetLocalPlayer()) {
            UI.PresentChoiceOfPlayers(choiceSet, Interpreter);
        }
    }
}