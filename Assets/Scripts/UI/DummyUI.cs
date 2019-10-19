using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DummyUI : UIAbstractor {

    public override void AddCardToHandDisplay (Card card) {
        Debug.Log("AddCardToHandDisplay");
    }

    public override void SetDeckText (int n) {
        Debug.Log("SetDeckText");
    }

    public override void UpdatePointDisplays (GamePlayer local, GamePlayer[] opponents) {
        Debug.Log("UpdatePointDisplays");
    }

    public override void RefreshOpponentDisplay (GamePlayer[] opponents) {
        Debug.Log("RefreshOpponentDisplay");
    }

    public override void DisplayOpponentCards (GamePlayer opponent, Table table, int opponentIndex) {
        Debug.Log("DisplayOpponentCards");
    }

    public override void SetDiscardText (int n) {
        Debug.Log("SetDiscardText");
    }

    public override void AddToDiscardDisplay (Card card) {
        Debug.Log("AddToDiscardDisplay");
    }

    public override void ToggleDisplayDiscard () {
        Debug.Log("ToggleDisplayDiscard");
    }

    public override void UpdateFlagsText (GameVariables variables) {
        Debug.Log("UpdateFlagsText");
    }

    public override void PresentChoiceOfPlayers(List<GamePlayer> players, Interpreter interpreter) {
        Debug.Log("PresentChoiceOfPlayers");
    }

    public override void PresentChoiceOfCards(List<Card> cards, Interpreter interpreter) {
        Debug.Log("PresentChoiceOfCards");
    }

    public override void RemoveChoiceDisplay () {
        Debug.Log("RemoveChoiceDisplay");
    }
	
}
