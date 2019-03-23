using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QueryRequest<T> {
    public Query<T> Query { get; set; }
    public QueryFilter Filter { get; set; }

    public QueryRequest(Query<T> q) {
        Query = q;
    }

}