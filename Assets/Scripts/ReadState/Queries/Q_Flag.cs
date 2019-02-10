using System.Collections.Generic;

[System.Serializable]
public class Q_Flag : Query {
    public override QueryResult Run(RunTimeValue target, GameController gameController){
        string key = (string) target.Evaluate();
        return new QueryResult(gameController.Variables.IsFlag(key));
    }

    public static QueryData QueryData = new QueryData() {
        name = "Look up a flag",
        fields = new List<FieldData>() {
            FieldLibrary.StringFieldData
        },
        query = new Q_Flag(),
        takesListOptions = false
    };
}