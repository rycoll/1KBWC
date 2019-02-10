using System.Collections.Generic;

[System.Serializable]
public class Q_Variable : Query {
    public override QueryResult Run(RunTimeValue target, GameController gameController) {
        string key = (string) target.Evaluate();
        return new QueryResult(gameController.Variables.GetVariable(key));
    }

    public static QueryData QueryData = new QueryData() {
        name = "Look up a variable",
        fields = new List<FieldData>(){
            FieldLibrary.StringFieldData
        },
        query = new Q_Variable(),
        takesListOptions = false
    };
}