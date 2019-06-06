public class CompareNum : Comparison {
    public int OperandA { get; set; }
    public int OperandB { get; set; }
    public ConditionOperator Operator { get; set; }

    public CompareNum(int num1, int num2, ConditionOperator op) {
        OperandA = num1;
        OperandB = num2;
        Operator = op;
    }

    public override bool Evaluate () {
        switch (Operator) {
            case ConditionOperator.EQUAL:
                return OperandA.Equals(OperandB);
            case ConditionOperator.NOT_EQUAL:
                return !(OperandA.Equals(OperandB));
            case ConditionOperator.MORE_THAN:
                return OperandA > OperandB;
            case ConditionOperator.AT_LEAST:
                return OperandA >= OperandB;
            case ConditionOperator.LESS_THAN:
                return OperandA < OperandB;
            case ConditionOperator.AT_MOST:
                return OperandA <= OperandB;
            default:
                return false;
        }
    }
}

