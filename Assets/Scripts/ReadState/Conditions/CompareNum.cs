public class CompareNum : Comparison {
    public RunTimeValue<int> OperandA { get; set; }
    public RunTimeValue<int> OperandB { get; set; }
    public ConditionOperator Operator { get; set; }

    public CompareNum(RunTimeValue<int> rtv1, RunTimeValue<int> rtv2, ConditionOperator op) {
        OperandA = rtv1;
        OperandB = rtv2;
        Operator = op;
    }

    public override bool Evaluate (GameController gameController) {
        int A = OperandA.Evaluate(gameController);
        int B = OperandB.Evaluate(gameController);
        switch (Operator) {
            case ConditionOperator.EQUAL:
                return A.Equals(B);
            case ConditionOperator.NOT_EQUAL:
                return !(A.Equals(B));
            case ConditionOperator.MORE_THAN:
                return A > B;
            case ConditionOperator.AT_LEAST:
                return A >= B;
            case ConditionOperator.LESS_THAN:
                return A < B;
            case ConditionOperator.AT_MOST:
                return A <= B;
            default:
                return false;
        }
    }
}

