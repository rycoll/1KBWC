using UnityEngine;
using System;
using System.Collections.Generic;

public class GameMaster {

    public Interpreter Bytes;
    public CardManager Cards;
    public GameFunctions Functions;
    public PlayerManager Players;
    public GameVariables Variables;

    public UIAbstractor UI;

    public ReadCallback queryCheck;
    
    public GameMaster () {
        Bytes = new Interpreter(this);
        Cards = new CardManager();
        Functions = new GameFunctions();
        Players = new PlayerManager(4);
        Variables = new GameVariables();

        // change this outside of testing
        UI = new DummyUI();

        queryCheck = QueryBeforeRead;
    }

    public void QueryBeforeRead () {
        Bytes.executeNext();
    }

    #region Player stuff

    public void SetPlayerPoints (GamePlayer player, int points) {
        player.Points = points;
        UI.UpdatePointDisplays(Players.GetLocalPlayer(), Players.GetOpponents());
    }

    #endregion

    #region Card stuff

    public Card FindCardById (string id) {
        Card card = Cards.FindCardById(id);
        if (card == null) {
            card = Players.FindCardById(id);
        }
        return card;
    }

    public void PlayerDrawCard(GamePlayer player) {
        GiveCardToPlayer(player, Cards.Deck.Pop());
    }

    public void GiveCardToPlayer(GamePlayer player, Card card)
    {
        if (player.AddToHand(card))
        {
            if (Players.IsLocalPlayer(player)) {
                UI.AddCardToHandDisplay(card);
            } else {
                UI.RefreshOpponentDisplay(Players.GetOpponents());
            }
        } else {
            // card could not be added, UI should give some feedback
        }
    }

    #endregion

    #region Stack stuff

    public void AddToStack(List<byte> bytes) {
        Bytes.push(bytes);
    }

    public GamePlayer ReadPlayerFromStack () {
        int index = Bytes.ReadPlayerLiteral(queryCheck);
        return Players.GetPlayer(index);
    }

    public Card ReadCardFromStack () {
        string id = Bytes.ReadCardLiteral(queryCheck);
        return FindCardById(id);
    }

    public List<GamePlayer> ReadPlayerListFromStack () {
        List<int> playerIndexList = Bytes.ReadPlayerList(queryCheck);
        List<GamePlayer> playerList = new List<GamePlayer>();
        foreach (int num in playerIndexList) {
            playerList.Add(Players.GetPlayer(num));
        }
        return playerList;
    }

    public List<Card> ReadCardListFromStack () {
        List<string> cardIDList = Bytes.ReadCardList(queryCheck);
        List<Card> cardList = new List<Card>();
        foreach (string id in cardIDList) {
            cardList.Add(FindCardById(id));
        }
        return cardList;
    }

    public void ExecuteEffects (List<byte> effects) {
        AddToStack(effects);
        while (Bytes.GetCurrentStackSize() > 0) {
            Bytes.next();
            // pause between effects here
        }
    }

    public void ExecuteNext() {
        Bytes.executeNext();
    }

    #endregion

}

public delegate void ReadCallback();