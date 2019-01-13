
[System.Serializable]
public class Card_MoistenArena : Card {
    public Card_MoistenArena(GameController gameController) {
        cardName = "Moisten Arena";
        overrideRuleText = "The arena becomes wet.";
        cardArt = null;

        RunTimeValue key = new RunTimeValue("Arena");
        RunTimeValue value = new RunTimeValue("WET");
        CardEffect MoistenEffect = new RE_SetVariable(key, value);
        AddEffect(MoistenEffect);
    }
}