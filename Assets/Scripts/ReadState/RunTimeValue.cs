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

    public RunTimeValue (Query q, object obj) {
        this.query = new QueryRequest(q, obj);
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

    // return null if it's a no-go
    public static List<object> TryExtractObjectList (RunTimeValue rtv) {
        List<object> list;
        object value = rtv.Evaluate();
        if (value.GetType().IsArray) {
            object[] array = value as object[];
            if (value != null) {
                list = new List<object>(array);
            } else {
                return null;
            }
        } else {
            list = value as List<object>;
            if (list == null) {
                return null;
            }
        }
        return list;
    }
}