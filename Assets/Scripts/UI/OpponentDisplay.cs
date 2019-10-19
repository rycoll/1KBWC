using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OpponentDisplay : MonoBehaviour
{
    public GameController GC;

    public Text nameText;
    public Text handText;
    public Text scoreText;
    public Image background;

    private GamePlayer opponent;

    public void RefreshDisplay() {
        if (opponent == null) {
            return;
        }
        nameText.text = opponent.Name;
        handText.text = opponent.Hand.GetNumCards().ToString() + " cards in hand";
        scoreText.text = opponent.Points.ToString() + " points";
        background.color = opponent.Colour;

        // set font color to black or white, depending on background brightness
        double lum = 0.2126  * opponent.Colour.r + 0.7152 * opponent.Colour.g + 0.0722 * opponent.Colour.b;
        Color fontColour = lum < 0.5 ? Color.white : Color.black;
        nameText.color = fontColour;
        handText.color = fontColour;
        scoreText.color = fontColour;
    }

    public void SetOpponent (GamePlayer player) {
        this.opponent = player;
    }

    public void OpenCards () {
        if (opponent == null) {
            Debug.Log("No player set!");
            return;
        }
        GC.DisplayOpponentCards(this.opponent);
    }
}
