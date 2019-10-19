using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIAbstractor {

    public abstract void AddCardToHandDisplay (Card card);

    public abstract void SetDeckText (int n);

    public abstract void UpdatePointDisplays (GamePlayer local, GamePlayer[] opponents);

    public abstract void RefreshOpponentDisplay (GamePlayer[] opponents);

    public abstract void DisplayOpponentCards (GamePlayer opponent, Table table, int opponentIndex);

    public abstract void SetDiscardText (int n);

    public abstract void AddToDiscardDisplay (Card card);

    public abstract void ToggleDisplayDiscard ();

    public abstract void UpdateFlagsText (GameVariables variables);

    public abstract void PresentChoiceOfPlayers(List<GamePlayer> players, Interpreter interpreter);

    public abstract void PresentChoiceOfCards(List<Card> cards, Interpreter interpreter);

    public abstract void RemoveChoiceDisplay ();
	
}
