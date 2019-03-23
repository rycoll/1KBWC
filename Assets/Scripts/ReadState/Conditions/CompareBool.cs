public class CompareBool : Comparison {
    public RunTimeValue<bool> Operand { get; set; }
    public bool checkValue { get; set; }
    public ConditionOperator Operator { get; set; }

    public CompareBool(RunTimeValue<bool> operand, bool check) {
        Operand = operand;
        checkValue = check;
    }

    public override bool Evaluate (GameController gameController) {
        return Operand.Evaluate(gameController) == checkValue;
    }
}

