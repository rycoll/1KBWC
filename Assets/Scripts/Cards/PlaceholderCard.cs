public class PlaceholderCard : Card
{
    public PlaceholderCard ()
    {
            cardName = "Placeholder Card";
            cardArt = null;
            overrideRuleText = "This is just a placeholder.";
            cardLocation = Location.HAND;
    }

    public override EffectResult Execute() {
        return new EffectResult();
    }
}