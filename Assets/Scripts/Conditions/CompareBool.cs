public class CompareBool : Comparison {
    public bool Operand { get; set; }
    public bool CheckValue { get; set; }
    public ConditionOperator Operator { get; set; }

    public CompareBool(bool operand, bool check) {
        Operand = operand;
        CheckValue = check;
    }

    public override bool Evaluate () {
        return Operand == CheckValue;
    }
}

