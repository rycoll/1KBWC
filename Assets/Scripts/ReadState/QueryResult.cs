using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueryResult {

    private static string RETURN = "result";
    private static string IS_LIST = "isList";
    private Hashtable resultTable;

    public QueryResult() {
        resultTable = new Hashtable();
    }

    public QueryResult(object returnValue) : this() {
        SetReturnValue(returnValue);
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

    public void SetIsList (bool b) {
        resultTable[IS_LIST] = b;
    }

    public bool IsList () {
        if (resultTable.ContainsKey(IS_LIST)) {
            return (bool) resultTable[IS_LIST];
        }
        return false;
    }
}