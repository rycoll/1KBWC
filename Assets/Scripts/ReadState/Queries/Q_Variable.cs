using System.Collections.Generic;

[System.Serializable]
public class Q_Variable : Query {
    public override QueryResult Run(RunTimeValue target, GameController gameController) {
        string key = (string) target.Evaluate();
        return new QueryResult(gameController.Variables.GetVariable(key));
    }

    private static QueryData QueryData;
    public static QueryData GetQueryData () {
        if (QueryData == null) {
            QueryData = new QueryData() {
                name = "Look up a variable",
                fields = new List<FieldData>(){
                    FieldLibrary.GetStringFieldData()
                },
                query = new Q_Variable(),
                takesListOptions = false
            };
        }
        return QueryData;
    }
}