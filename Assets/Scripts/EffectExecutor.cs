using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectExecutor : MonoBehaviour
{
    
    private GameController gameController;
    private const int MAX_EXECUTION_LOOPS = 1000;

    void Start()
    {
        gameController = GetComponent<GameController>();
        RunTimeValue.Executor = this;
        Listener.Executor = this;
    }

    public List<EffectResult> Execute(List<CardEffect> cardEffects) {
        int numLoops = 0;
        while (cardEffects.Count > 0 && numLoops++ < MAX_EXECUTION_LOOPS) {
            CardEffect current = cardEffects[0];
            cardEffects.RemoveAt(0);

            switch (current.GetType().Module.ToString()) {
                case "ControlEffect":
                    ControlEffect control = (ControlEffect) current;
                    cardEffects.InsertRange(0, control.Compile());
                    break;
                case "RegularEffect":
                    RegularEffect effect = (RegularEffect) current;
                    effect.Run(gameController);
                    break;
            }
        }

        return new List<EffectResult>();
    }

    public QueryResult RunQuery (QueryRequest request) {
        // Run needs request.Filter and request.SecondaryQuery to run secondary functions
        // alternatively, those could be run here.
        QueryResult result =  request.Query.Run(request.Target_Ref, gameController);
        if (result.IsList()) {
            result = Query.RunSecondaryQueries(request, result);
        }
        return result;
    }
}
