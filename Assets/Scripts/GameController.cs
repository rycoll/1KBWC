using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// this needs to be networked or whatever
public class GameController : MonoBehaviour {

    private BinaryCardLoader BinaryLoader;
    private UIController UI;
    public Deck Deck { get; set;}
    public Deck Discard { get; set; }
    public Table Table { get; set; }
    public GameVariables Variables { get; set; }
    public GameListeners Listeners { get; set; }
    private GamePlayer[] players;
    private int activePlayerIndex;

    private void Start() {
        BinaryLoader = this.GetComponent<BinaryCardLoader>();
        UI = this.GetComponent<UIController>();
        Deck = new Deck();
        Discard = new Deck();
        Table = new Table();
        Variables = new GameVariables();
        Listeners = new GameListeners();
        players = new GamePlayer[] {
            new GamePlayer("Player1"), 
            new GamePlayer("Player2"),
            new GamePlayer("Player3")
        };
        activePlayerIndex = 0;

        GiveCardToPlayer(players[0], new Card_Draw2(this));
        GiveCardToPlayer(players[0], new Card_Gain1Point(this));
        GiveCardToPlayer(players[0], new Card_MassGainPoint(this));
        GiveCardToPlayer(players[0], new Card_MoistenArena(this));
        GiveCardToPlayer(players[0], new Card_FloorSuck(this));
        GiveCardToPlayer(players[0], new Card_Gift(this));
        GiveCardToPlayer(players[1], new Card_Gain1Point(this));
        GiveCardToPlayer(players[2], new Card_Gain1Point(this));

        UI.RefreshOpponentDisplay(this.GetOpponents());
        UI.DisplayOpponentCards(players[1]);
	}

    public GamePlayer GetActivePlayer ()
    {
        return players[activePlayerIndex];
    }

    public GamePlayer GetLocalPlayer () {
        return (GamePlayer) players.Where(player => IsLocalPlayer(player)).First();
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
        player.Hand.RemoveCard(card);
        // clone card effects instead??
        ExecuteEffects(card.Effects);
        AddToDiscard(card, DECK_LOCATION.TOP);
        // rack up animations etc asynchronously, play them, THEN continue on
    }

    public void Next() {
        // make sure animations aren't still happening or whatever
        if (CheckForWinner()) {
            return;
        }

        // if there are more effects,
        // run the next effect

        // if there are no more effects
        PassTurn();
    }

    public bool CheckForWinner () {
        foreach (GamePlayer player in players) {
            if (player.WinCondition.Evaluate()) {
                Debug.Log(player.Name + " wins!");
                return true;
            }
        }
        return false;
    }

    public void AddToDeck(Card card, DECK_LOCATION loc) {
        Deck.AddCard(card, loc);
        UI.SetDeckText(Deck.GetSize());
    }

    public void AddToDiscard(Card card, DECK_LOCATION loc) {
        Discard.AddCard(card, loc);
        UI.AddToDiscardDisplay(card);
        UI.SetDiscardText(Discard.GetSize());
    }

    public QueryResult RunQuery(QueryRequest request) {
        return EffectExecutor.RunQuery(request);
    }

    public List<EffectResult> ExecuteEffects (List<CardEffect> list) {
        return EffectExecutor.Execute(list);
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

    public void PresentChoice (List<object> choiceSet, ChoiceCallback callback) {
        if (choiceSet.Count == 0) {
            callback(null, this);
        }

        // currently always presents to active player
        if (GetActivePlayer() == GetLocalPlayer()) {
            try {
                List<GamePlayer> playerList = choiceSet.Cast<GamePlayer>().ToList();
                UI.PresentChoiceOfPlayers(playerList, callback);
                return;
            } catch (InvalidCastException) {}

            try {
                List<Card> cardList = choiceSet.Cast<Card>().ToList();
                UI.PresentChoiceOfCards(cardList, callback);
                return;
            } catch (InvalidCastException) {}

            Debug.Log("ERROR: wasn't sure what kind of choice this was!");
            Debug.Log(choiceSet);
            callback(null, this);
        }
    }
}
