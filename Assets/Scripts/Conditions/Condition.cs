using System;

[System.Serializable]
public class Condition {
    public Comparison comparison { get; set; }

    public Condition(Comparison comp) {
        this.comparison = comp;
    }

    public Condition(int numA, int numB, ConditionOperator op) {
        this.comparison = new CompareNum(numA, numB, op);
    }

    public Condition(bool b, bool check, ConditionOperator op) {
        this.comparison = new CompareBool(b, check, op);
    }
    
    public bool Evaluate () {
        return comparison.Evaluate();
    }
}

public enum ConditionType {
    NUM, BOOL, STRING
}

public enum ConditionOperator {
    EQUAL, NOT_EQUAL, MORE_THAN, AT_LEAST, LESS_THAN, AT_MOST
};