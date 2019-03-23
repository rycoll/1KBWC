using System.Collections.Generic;

[System.Serializable]
public class Q_Counter : Query<int> {
    public RunTimeValue<string> target;
    public override RunTimeValue<int> Run(GameController gameController) {
        string key = target.Evaluate(gameController);
        return new RunTimeValue<int>(gameController.Variables.GetCounter(key));
    }

    private static QueryData QueryData;
    public static QueryData GetQueryData () {
        if (QueryData == null) {
            QueryData = new QueryData() {
                name = "Look up counter",
                fields = new List<FieldData>() {
                    FieldLibrary.GetStringFieldData()
                },
                takesListOptions = false
            };
        }
        return QueryData;
    }
}