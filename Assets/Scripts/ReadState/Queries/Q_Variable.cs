using System.Collections.Generic;

[System.Serializable]
public class Q_Variable : Query<string> {
    public RunTimeValue<string> target;
    public override RunTimeValue<string> Run(GameController gameController) {
        string key = target.Evaluate(gameController);
        return new RunTimeValue<string>(gameController.Variables.GetVariable(key));
    }

    private static QueryData QueryData;
    public static QueryData GetQueryData () {
        if (QueryData == null) {
            QueryData = new QueryData() {
                name = "Look up a variable",
                fields = new List<FieldData>(){
                    FieldLibrary.GetStringFieldData()
                },
                takesListOptions = false
            };
        }
        return QueryData;
    }
}