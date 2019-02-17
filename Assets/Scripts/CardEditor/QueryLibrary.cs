using System;
using System.Collections.Generic;
using System.Linq;

public static class QueryLibrary {
    public static QueryData[] AllQueryData = new QueryData[] {
        Q_ActivePlayer.GetQueryData(),
        Q_Counter.GetQueryData(),
        Q_DeckCards.GetQueryData(),
        Q_DeckSize.GetQueryData(),
        Q_DiscardCards.GetQueryData(),
        Q_DiscardSize.GetQueryData(),
        Q_Flag.GetQueryData(),
        Q_Opponents.GetQueryData(),
        Q_PlayerDrawSize.GetQueryData(),
        Q_PlayerHand.GetQueryData(),
        Q_PlayerMaxHand.GetQueryData(),
        Q_PlayerName.GetQueryData(),
        Q_PlayerPoints.GetQueryData(),
        Q_Players.GetQueryData(),
        Q_TableCards.GetQueryData(),
        Q_TableSize.GetQueryData(),
        Q_Variable.GetQueryData(),
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

public class QueryData {
    public string name;
    public List<FieldData> fields;
    public Query query;
    public bool takesListOptions;
}
