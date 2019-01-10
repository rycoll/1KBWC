using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// this needs to be networked or whatever
public class GameController : MonoBehaviour {

    private UIController UI;
    private EffectExecutor Executor;
    public Deck Deck { get; set;}
    public Deck Discard { get; set; }
    public Table Table { get; set; }
    public GameVariables Variables { get; set; }
    public GameListeners Listeners { get; set; }
    private GamePlayer[] players;
    private int activePlayerIndex;

    private void Start() {
        UI = this.GetComponent<UIController>();
        Executor = this.GetComponent<EffectExecutor>();
        Deck = new Deck();
        Discard = new Deck();
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

        GiveCardToPlayer(players[0], new Card_Gain1Point(this));
        GiveCardToPlayer(players[0], new Card_MassGainPoint(this));
        GiveCardToPlayer(players[0], new Card_MoistenArena(this));
        GiveCardToPlayer(players[0], new Card_FloorSuck(this));
        GiveCardToPlayer(players[0], new Card_FloorSuck(this));
        GiveCardToPlayer(players[1], new Card_Gain1Point(this));
        GiveCardToPlayer(players[2], new Card_Gain1Point(this));
        GiveCardToPlayer(players[2], new Card_Gain1Point(this));
        GiveCardToPlayer(players[2], new Card_Gain1Point(this));
        GiveCardToPlayer(players[3], new Card_Gain1Point(this));

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
        //GiveCardToPlayer(GetActivePlayer(), Deck.Pop());
        GiveCardToPlayer(GetActivePlayer(), new Card_Gain1Point(this));
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
        PassTurn();
    }

    public void AddToDiscard(Card card, DECK_LOCATION loc) {
        Discard.AddCard(card, loc);
        UI.AddToDiscardDisplay(card);
        UI.SetDiscardText(Discard.GetSize());
    }

    public QueryResult RunQuery(QueryRequest request) {
        return Executor.RunQuery(request);
    }

    public List<EffectResult> ExecuteEffects (List<CardEffect> list) {
        return Executor.Execute(list);
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
        Debug.Log(players[0].Points);
        UI.UpdatePointDisplays();
    }
}
