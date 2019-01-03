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
}