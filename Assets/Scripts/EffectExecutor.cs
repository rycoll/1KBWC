using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectExecutor : MonoBehaviour
{
    private static GameController GameController;
    private const int MAX_EXECUTION_LOOPS = 1000;

    void Awake()
    {
        GameController = GetComponent<GameController>();
    }

    public static List<EffectResult> Execute(List<CardEffect> cardEffects) {
        int numLoops = 0;
        while (cardEffects.Count > 0 && numLoops++ < MAX_EXECUTION_LOOPS) {
            CardEffect current = cardEffects[0];
            cardEffects.RemoveAt(0);

            if (typeof(ControlEffect).IsInstanceOfType(current)) {
                // control effect
                ControlEffect control = (ControlEffect) current;
                cardEffects.InsertRange(0, control.Compile());
            } else {
                // regular effect
                RegularEffect effect = (RegularEffect) current;
                effect.Run(GameController);
            }
        }

        return new List<EffectResult>();
    }

    public static QueryResult RunQuery (QueryRequest request) {
        // Run needs request.Filter and request.SecondaryQuery to run secondary functions
        // alternatively, those could be run here.
        QueryResult result =  request.Query.Run(request.Target_Ref, GameController);
        if (result.IsList()) {
            result = Query.RunSecondaryQueries(request, result);
        }
        return result;
    }
}
