using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Query {
    public abstract QueryResult Run(object target, GameController gameController);

    public static QueryResult RunSecondaryQueries (QueryRequest request, QueryResult result) {
        // run filter
        // run list query
        return RunListQuery(request, result);
    }

    private static void RunListFilter<T> (List<T> list) {
        // not clear how this is going to work :(
    }

    private static QueryResult RunListQuery(QueryRequest request, QueryResult result) {
        // casting to List<object> seems... suspect
        RunListFilter((List<object>)result.GetReturnValue());
        if (request.SecondaryQuery != null) {
            object listQueryResult = RunListQuery(
                (List<object>)result.GetReturnValue(), 
                request.SecondaryQuery
            );
            result.SetReturnValue(listQueryResult);
        }
        // don't really need to return, because of referencing?
        // could use out keyword in parameter... but also not necessary?
        return result;
    }

    private static object RunListQuery<T> (List<T> list, SecondaryQuery query) {
        switch (query.QueryType) {
            case LIST_QUERY.LIST:
                return list;
            case LIST_QUERY.SIZE:
                return list.Count;
            case LIST_QUERY.RAND_ITEM:
                return list[Random.Range(0, list.Count)];
        }
        return null;
    }
}