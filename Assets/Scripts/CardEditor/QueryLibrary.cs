using System;
using System.Collections.Generic;
using System.Linq;

public static class QueryLibrary {
    private static QueryData[] AllQueryData;
    public static QueryData[] GetAllQueryData () {
        if (AllQueryData == null) {
            AllQueryData = new QueryData[] {
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
        }
        return AllQueryData;
    }

    private static QueryData NullQueryData;
    public static QueryData GetNullQueryData () {
        if (NullQueryData == null) {
            NullQueryData= new QueryData {
                name = "NULL",
                fields = new List<FieldData>(),
                takesListOptions = false
            };
        }
        return NullQueryData;
    }

    public static QueryData GetQueryDataByName (string name) {
        QueryData[] list = GetAllQueryData().Where((query, index) => query.name.Equals(name)).ToArray();
        if (list != null && list.Length > 0) {
            return list[0];
        } else {
            return GetNullQueryData();
        }
    }
}
