public class Field_Number : Field<int> {
    public override int Evaluate(GameController gameController) {
        if (useLookup) {
            return DynamicValueTable.GetNumber(lookup);
        }
        return Value.Evaluate(gameController);
    }
}