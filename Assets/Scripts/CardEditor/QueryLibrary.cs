using System;
using System.Collections.Generic;
using System.Linq;

public static class QueryLibrary {
    private static QueryData[] AllQueryData;
    public static QueryData[] GetAllQueryData () {
        if (AllQueryData == null) {
            AllQueryData = new QueryData[0];
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
