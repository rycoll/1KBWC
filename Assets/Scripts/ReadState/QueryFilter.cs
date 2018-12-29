using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FILTER_OPERATOR {LESS, MORE, EQUAL}

public class QueryFilter {
    public FILTER_OPERATOR Operator { get; set; }
    public object Operand1 { get; set; }
    public object Operand2 { get; set; }

    public QueryFilter (object op1, object op2, FILTER_OPERATOR op) {
        Operand1 = op1;
        Operand2 = op2;
        Operator = op;
    }
}