public class Card_Draw2 : Card {
    public Card_Draw2(GameController game) {
        cardName = "Draw Two";
        overrideRuleText = "Draw 2 cards.";
        cardArt = null;

        QueryRequest<GamePlayer> currPlayerReq = new QueryRequest<GamePlayer>(
            new Q_ActivePlayer()
        );
        RunTimeValue<GamePlayer> player = new RunTimeValue<GamePlayer>(currPlayerReq);

        CardEffect effect = new RE_PlayerDraw(player, new RunTimeValue<int>(2));
        AddEffect(effect);
    }

}