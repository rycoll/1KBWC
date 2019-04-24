public class Field_Player : Field<GamePlayer> {
    public override GamePlayer Evaluate(GameController gameController) {
        if (useLookup) {
            return DynamicValueTable.GetPlayer(lookup);
        }
        return Value.Evaluate(gameController);
    }
}