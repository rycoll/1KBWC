using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OpponentDisplay : MonoBehaviour
{
    public Text nameText;
    public Text handText;
    public Text scoreText;

    public void RefreshDisplay(string name, string hand, string score) {
        nameText.text = name;
        handText.text = hand;
        scoreText.text = score;
    }
}
