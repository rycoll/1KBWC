using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RunTimeValue<T> {

    private QueryRequest<T> queryRequest;
    private T value;

    public RunTimeValue (QueryRequest<T> q) {
        this.queryRequest = q;
    }

    public RunTimeValue (T obj) {
        this.value = obj;
    }

    public T Evaluate (GameController gameController) {
        if (queryRequest != null) {
            Query<T> query = queryRequest.Query;
            return query.Run(gameController).Evaluate(gameController);
        }
        return value;
    }
    
    /*
    // probably delete this soon
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
    */
}