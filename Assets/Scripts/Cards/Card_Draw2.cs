public class Card_Draw2 : Card {
    public Card_Draw2(GameController game) {
        cardName = "Draw Two";
        overrideRuleText = "Draw 2 cards.";
        cardArt = null;

        QueryRequest currPlayerReq = new QueryRequest(
            new Q_ActivePlayer(), null
        );
        RunTimeValue player = new RunTimeValue(currPlayerReq);

        CardEffect effect = new RE_PlayerDraw(player, new RunTimeValue(2));
        AddEffect(effect);
    }

}