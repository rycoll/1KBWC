using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueryResult {

    private static string RETURN = "result";
    private Hashtable resultTable;

    public QueryResult() {
        resultTable = new Hashtable();
    }

    public void SetReturnValue(object returnValue) {
        resultTable[RETURN] = returnValue;
    }

    public object GetReturnValue() {
        if (resultTable.ContainsKey(RETURN)) {
            return resultTable[RETURN];
        }
        return null;
    }
}