using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QUERY_TARGET {
    FLAG, VARIABLE, COUNTER,
    DECK_SIZE, DECK_CARDS, 
    DISCARD_SIZE, DISCARD_CARDS,
    TABLE_SIZE, TABLE_CARDS,
    PLAYERS, ACTIVE_PLAYER, OPPONENTS, 
    PLAYER_NAME, PLAYER_HAND, PLAYER_POINTS, PLAYER_DRAW_SIZE, PLAYER_MAX_HAND
};

public class QueryRequest {
    public Query Query { get; set; }
    public QueryFilter Filter { get; set; }

    public object Target_Ref { get; set; }
    public SecondaryQuery SecondaryQuery { get; set; }

    public QueryRequest(Query q, object target) {
        Query = q;
        Target_Ref = target;
    } 
}

public enum LIST_QUERY { SIZE, LIST, RAND_ITEM };
public class SecondaryQuery {
    public LIST_QUERY QueryType { get; set; }
}