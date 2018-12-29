using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunTimeValue {

    public static EffectExecutor Executor;

    private QueryRequest query = null;
    private object value = null;

    public RunTimeValue (QueryRequest q) {
        this.query = q;
    }

    public RunTimeValue (object obj) {
        this.value = obj;
    }

    public object Evaluate () {
        if (query != null) {
            QueryResult result = Executor.RunQuery(query);
            return result.GetReturnValue();
        }
        return value;
    }
}