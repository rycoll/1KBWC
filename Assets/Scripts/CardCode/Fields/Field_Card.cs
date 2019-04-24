public class Field_Card : Field<Card> {
    public override Card Evaluate(GameController gameController) {
        if (useLookup) {
            return DynamicValueTable.GetCard(lookup);
        }
        return Value.Evaluate(gameController);
    }
}