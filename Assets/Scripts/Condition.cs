using System;

public class Condition {
    public RunTimeValue OperandA { get; set; }
    public RunTimeValue OperandB { get; set; }
    public ConditionOperator Operator { get; set; }

    public Condition(RunTimeValue rtv1, RunTimeValue rtv2, ConditionOperator op) {
        OperandA = rtv1;
        OperandB = rtv2;
        Operator = op;
    }
    
    public bool Evaluate () {
        object A = OperandA.Evaluate();
        object B = OperandB.Evaluate();
        switch (Operator) {
            case ConditionOperator.EQUAL:
                return A.Equals(B);
            case ConditionOperator.NOT_EQUAL:
                return !(A.Equals(B));
            case ConditionOperator.MORE_THAN:
                return Convert.ToDouble(A) > Convert.ToDouble(B);
            case ConditionOperator.AT_LEAST:
                return Convert.ToDouble(A) >= Convert.ToDouble(B);
            case ConditionOperator.LESS_THAN:
                return Convert.ToDouble(A) < Convert.ToDouble(B);
            case ConditionOperator.AT_MOST:
                return Convert.ToDouble(A) <= Convert.ToDouble(B);
            default:
                return false;
        }
    }
}

public enum ConditionOperator {
    EQUAL, NOT_EQUAL,

    /* NOTE: these operators REQUIRE RunTimeValues that return NUMBERS */
    MORE_THAN, AT_LEAST, LESS_THAN, AT_MOST
};