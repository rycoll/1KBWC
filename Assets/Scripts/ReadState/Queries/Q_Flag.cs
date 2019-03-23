using System.Collections.Generic;

[System.Serializable]
public class Q_Flag : Query<bool> {
    public RunTimeValue<string> target;

    public override RunTimeValue<bool> Run(GameController gameController){
        string key = target.Evaluate(gameController);
        return new RunTimeValue<bool>(gameController.Variables.IsFlag(key));
    }
    
    private static QueryData QueryData;
    public static QueryData GetQueryData () {
        if (QueryData == null) {
            QueryData = new QueryData() {
                name = "Look up a flag",
                fields = new List<FieldData>() {
                    FieldLibrary.GetStringFieldData()
                },
                takesListOptions = false
            };
        }
        return QueryData;
    }
}