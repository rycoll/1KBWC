using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QueryRequest {
    public Query Query { get; set; }
    public QueryFilter Filter { get; set; }

    public RunTimeValue Target_Ref { get; set; }
    public SecondaryQuery SecondaryQuery { get; set; }

    public QueryRequest(Query q) {
        Query = q;
        Target_Ref = null;
    }

    public QueryRequest(Query q, RunTimeValue target) {
        Query = q;
        Target_Ref = target;
    }

    public QueryRequest(Query q, object o) {
        Query = q;
        Target_Ref = new RunTimeValue(o);
    }
}

public enum LIST_QUERY { SIZE, LIST, RAND_ITEM };

[System.Serializable]
public class SecondaryQuery {
    public LIST_QUERY QueryType { get; set; }
}