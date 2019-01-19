using System;
using UnityEngine;

[System.Serializable]
public class RE_PlayerDraw : RegularEffect {

    private RunTimeValue Player { get; set; }
    private RunTimeValue NumCards { get; set; }

    public RE_PlayerDraw (RunTimeValue player, RunTimeValue num) {
        Player = player;
        NumCards = num;
    }
    public override void Run(GameController gameController) {
        GamePlayer player = Player.Evaluate() as GamePlayer;
        if (CheckTypeError(Player, player)) {
            Done(gameController);
            return;
        }
        int num = 0;
        try {
            num = (int) NumCards.Evaluate();
        } catch (InvalidCastException) {
            Debug.Log("Bad cast!");
            Debug.Log(NumCards.Evaluate());
            Done(gameController);
            return;
        }

        for (int i = 0; i < num; i++) {
            gameController.PlayerDrawCard(player);
        }

        Done(gameController);
    }

    public override void HandleInput(object obj) {
        if (!IgnoreInput) {
            GamePlayer playerObj = obj as GamePlayer;
            if (playerObj != null) {
                Player = new RunTimeValue(playerObj);
                return;
            }
            try {
                int num = (int) obj;
                NumCards = new RunTimeValue(num);
            } catch (InvalidCastException) {}
        }
    }
}