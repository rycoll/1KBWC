public class CompareBool : Comparison {
    public bool Operand { get; set; }
    public bool CheckValue { get; set; }
    public ConditionOperator Operator { get; set; }

    public CompareBool(bool operand, bool check, ConditionOperator op) {
        Operand = operand;
        CheckValue = check;

        if (op == ConditionOperator.EQUAL || op == ConditionOperator.NOT_EQUAL) {
            Operator = op;
        } else {
            Operator = ConditionOperator.EQUAL;
        }
    }

    public override bool Evaluate () {
        if (Operator == ConditionOperator.EQUAL) {
            return Operand == CheckValue;
        }
        return Operand != CheckValue;
    }
}

