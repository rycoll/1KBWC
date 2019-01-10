using UnityEngine;

public class RE_AddToHand : RegularEffect {

    private RunTimeValue Player { get; set; }
    private RunTimeValue Card { get; set; }

    public RE_AddToHand (RunTimeValue player, RunTimeValue card) {
        Player = player;
        Card = card;
    }
    public override void Run(GameController gameController) {
        GamePlayer player = Player.Evaluate() as GamePlayer;
        Card card = Card.Evaluate() as Card;

        if (CheckTypeError(Player, player)) return;
        if (CheckTypeError(Card, card)) return;
        
        gameController.GiveCardToPlayer(player, card);
    }

    public override void HandleInput(object obj) {
        if (!IgnoreInput) {
            GamePlayer playerObj = obj as GamePlayer;
            if (playerObj != null) {
                Player = new RunTimeValue(playerObj);
                return;
            }
            Card cardObj = obj as Card;
            if (cardObj != null) {
                Card = new RunTimeValue(cardObj);
            }
        }
    }
}