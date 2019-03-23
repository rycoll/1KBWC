using System;

[System.Serializable]
public class Condition {
    public Comparison comparison { get; set; }

    public Condition(Comparison comp) {
        this.comparison = comp;
    }

    public Condition(RunTimeValue<int> numA, RunTimeValue<int> numB, ConditionOperator op) {
        this.comparison = new CompareNum(numA, numB, op);
    }

    public Condition(RunTimeValue<bool> b, bool check) {
        this.comparison = new CompareBool(b, check);
    }
    
    public bool Evaluate (GameController gameController) {
        return comparison.Evaluate(gameController);
    }
}

public enum ConditionOperator {
    EQUAL, NOT_EQUAL,

    /* NOTE: these operators REQUIRE RunTimeValues that return NUMBERS */
    MORE_THAN, AT_LEAST, LESS_THAN, AT_MOST
};