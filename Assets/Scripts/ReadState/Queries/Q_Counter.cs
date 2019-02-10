using System.Collections.Generic;

[System.Serializable]
public class Q_Counter : Query {
    public override QueryResult Run(RunTimeValue target, GameController gameController) {
        string key = (string) target.Evaluate();
        return new QueryResult(gameController.Variables.GetCounter(key));
    }

    public static QueryData QueryData = new QueryData() {
        name = "Look up counter",
        fields = new List<FieldData>() {
            FieldLibrary.StringFieldData
        },
        query = new Q_Counter(),
        takesListOptions = false
    };
}