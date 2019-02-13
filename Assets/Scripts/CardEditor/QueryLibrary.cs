using System;
using System.Collections.Generic;
using System.Linq;

public static class QueryLibrary {
    public static QueryData[] AllQueryData = new QueryData[] {
        Q_ActivePlayer.QueryData,
        Q_Counter.QueryData,
        Q_DeckCards.QueryData,
        Q_DeckSize.QueryData,
        Q_DiscardCards.QueryData,
        Q_DiscardSize.QueryData,
        Q_Flag.QueryData,
        Q_Opponents.QueryData,
        Q_PlayerDrawSize.QueryData,
        Q_PlayerHand.QueryData,
        Q_PlayerMaxHand.QueryData,
        Q_PlayerName.QueryData,
        Q_PlayerPoints.QueryData,
        Q_Players.QueryData,
        Q_TableCards.QueryData,
        Q_TableSize.QueryData,
        Q_Variable.QueryData,
    };

    public static QueryData NullQueryData = new QueryData {
        name = "NULL",
        fields = new List<FieldData>(),
        query = new Q_ActivePlayer(),
        takesListOptions = false
    };  

    public static QueryData GetQueryDataByName (string name) {
        QueryData[] list = AllQueryData.Where((query, index) => query.name.Equals(name)).ToArray();
        if (list != null && list.Length > 0) {
            return list[0];
        } else {
            return NullQueryData;
        }
    }
}

public struct QueryData {
    public string name;
    public List<FieldData> fields;
    public Query query;
    public bool takesListOptions;
}
