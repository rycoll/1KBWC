using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// this needs to be networked or whatever
public class GameController : MonoBehaviour {
    protected UIController UI;
    protected GameMaster GM;
    public GameListeners Listeners { get; set; }

    protected void Start() {
        UI = this.GetComponent<UIController>();
        GM = new GameMaster();
        Listeners = new GameListeners();

        // need to crank up display here
	}

    public void PassTurn()
    {
        GM.Players.PassTurnIndex();
        DrawPhase();
        GamePlayer activePlayer = GM.Players.GetActivePlayer();
        if (GM.Players.IsLocalPlayer(activePlayer)) {
            // indicate that it's your turn
        } else {
            // indicate whose turn it is

            // for now, we'll just have NPCs play the first card in their hand
            if (activePlayer.Hand.GetNumCards() > 0) {
                PlayCard(activePlayer, activePlayer.Hand.GetCards()[0]);
            } else {
                PassTurn();
            }
        }
    }

    public void DrawPhase()
    {
        GM.PlayerDrawCard(GM.Players.GetActivePlayer());
        UI.SetDeckText(GM.Cards.Deck.GetSize());
    }

    public void PlayCard(GamePlayer player, Card card) {
        player.Hand.RemoveCard(card.GetID());
        // clone card effects instead??
        GM.ExecuteEffects(card.Effects);
        GM.Cards.Discard.AddCard(card, DeckLocation.TOP);
        // rack up animations etc asynchronously, play them, THEN continue on
    }
    
    public bool EvaluatePlayerWinCondition (GamePlayer player) {
        GM.AddToStack(player.WinCondition);
        Condition condition = GM.Bytes.ReadConditionLiteral(GM.Bytes.skipToNext);
        return condition.Evaluate();
    }

    public bool CheckForWinner () {
        foreach (GamePlayer player in GM.Players.GetPlayers()) {
            if (EvaluatePlayerWinCondition(player)) {
                Debug.Log(player.Name + " wins!");
                return true;
            }
        }
        return false;
    }

    public void AddToDeck(Card card, DeckLocation loc) {
        GM.Cards.Deck.AddCard(card, loc);
        UI.SetDeckText(GM.Cards.Deck.GetSize());
    }

    public void AddToDiscard(Card card, DeckLocation loc) {
        GM.Cards.Discard.AddCard(card, loc);
        UI.AddToDiscardDisplay(card);
        UI.SetDiscardText(GM.Cards.Discard.GetSize());
    }

    public void PresentChoiceOfCards (List<Card> choiceSet) {
        if (choiceSet.Count == 0) {
            GM.Bytes.CardChoiceCallback(null);
        }
        // currently always presents to active player
        if (GM.Players.IsLocalPlayer(GM.Players.GetActivePlayer())) {
            UI.PresentChoiceOfCards(choiceSet, GM.Bytes);
        }
    }

    public void PresentChoiceOfPlayers (List<GamePlayer> choiceSet) {
        if (choiceSet.Count == 0) {
            GM.Bytes.PlayerChoiceCallback(null);
        }
        
        // currently always presents to active player
        if (GM.Players.IsLocalPlayer(GM.Players.GetActivePlayer())) {
            UI.PresentChoiceOfPlayers(choiceSet, GM.Bytes);
        }
    }

    public GamePlayer GetLocalPlayer() {
        return GM.Players.GetLocalPlayer();
    }
    public List<string> GetFlags () {
        return GM.Variables.GetFlags();
    }
    public Dictionary<string, string> GetVariables () {
        return GM.Variables.GetVariables();
    }
    public Dictionary<string, int> GetCounters () {
        return GM.Variables.GetCounters();
    }

    public void DisplayOpponentCards(GamePlayer opponent) {
        UI.DisplayOpponentCards(
            opponent,
            GM.Cards.Table,
            GM.Players.GetIndexOfPlayer(opponent)
        );
    }
}