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
    }

    public List<EffectResult> Execute(List<CardEffect> cardEffects) {
        int numLoops = 0;
        while (cardEffects.Count > 0 && numLoops++ < MAX_EXECUTION_LOOPS) {
            CardEffect current = cardEffects[0];
            cardEffects.RemoveAt(0);

            switch (current.GetType().Module.ToString()) {
                case "ControlEffect":
                    ControlEffect control = (ControlEffect) current;
                    switch(control.GetControlType()) {
                        case ControlType.IF:
                            cardEffects.InsertRange(0, ((CE_If) control).Compile());
                            break;
                        case ControlType.FOR_LOOP:
                            cardEffects.InsertRange(0, ((CE_ForLoop) control).Compile());
                            break;
                    }
                    break;
                case "Effect":
                    break;
            }
        }

        return new List<EffectResult>();
    }

    public QueryResult RunQuery (QueryRequest request) {
        QueryResult result = new QueryResult();
        bool selectedArray = false;
        switch (request.Target) {
            case QUERY_TARGET.FLAG: {
                    string key = (string) request.Target_Ref;
                    result.SetReturnValue(gameController.Variables.IsFlag(key));
                }
                break;
            case QUERY_TARGET.VARIABLE: {
                    string key = (string) request.Target_Ref;
                    result.SetReturnValue(gameController.Variables.GetVariable(key));
                }
                break;
            case QUERY_TARGET.COUNTER: {
                    string key = (string) request.Target_Ref;
                    result.SetReturnValue(gameController.Variables.GetCounter(key));
                }
                break;
            case QUERY_TARGET.DECK_SIZE:
                result.SetReturnValue(gameController.Deck.GetSize());
                break;
            case QUERY_TARGET.DECK_CARDS:
                result.SetReturnValue(gameController.Deck.GetCards());
                selectedArray = true;
                break;
            case QUERY_TARGET.DISCARD_SIZE:
                result.SetReturnValue(gameController.Discard.GetSize());
                break;
            case QUERY_TARGET.DISCARD_CARDS:
                result.SetReturnValue(gameController.Discard.GetCards());
                selectedArray = true;
                break;
            case QUERY_TARGET.TABLE_SIZE:
                result.SetReturnValue(gameController.Table.GetSize());
                break;
            case QUERY_TARGET.TABLE_CARDS:
                result.SetReturnValue(gameController.Table.GetCards());
                selectedArray = true;
                break;
            case QUERY_TARGET.PLAYERS:
                result.SetReturnValue(gameController.GetPlayers());
                selectedArray = true;
                break;
            case QUERY_TARGET.ACTIVE_PLAYER:
                result.SetReturnValue(gameController.GetActivePlayer());
                break;
            case QUERY_TARGET.OPPONENTS:
                result.SetReturnValue(gameController.GetOpponents());
                selectedArray = true;
                break;
            case QUERY_TARGET.PLAYER_NAME: {
                    GamePlayer player = (GamePlayer) request.Target_Ref;
                    result.SetReturnValue(player.Name);
                }
                break;
            case QUERY_TARGET.PLAYER_HAND: {
                    GamePlayer player = (GamePlayer) request.Target_Ref;
                    result.SetReturnValue(player.Hand.GetCards());
                }
                selectedArray = true;
                break;
            case QUERY_TARGET.PLAYER_POINTS: {
                    GamePlayer player = (GamePlayer) request.Target_Ref;
                    result.SetReturnValue(player.Points);
                }
                break;
            case QUERY_TARGET.PLAYER_MAX_HAND: {
                    GamePlayer player = (GamePlayer) request.Target_Ref;
                    result.SetReturnValue(player.Hand.MaxHandSize);
                }
                break;
            case QUERY_TARGET.PLAYER_DRAW_SIZE: {
                    GamePlayer player = (GamePlayer) request.Target_Ref;
                    result.SetReturnValue(player.DrawPerTurn);
                }
                break;
            default:
                break;
        }

        if (selectedArray) {
            // casting to List<object> seems... suspect
            RunListFilter((List<object>)result.GetReturnValue());
            if (request.SecondaryQuery != null) {
                object listQueryResult = RunListQuery(
                    (List<object>)result.GetReturnValue(), 
                    request.SecondaryQuery
                );
                result.SetReturnValue(listQueryResult);
            }
        }
        return result;
    }

    public void RunListFilter<T> (List<T> list) {
        // not clear how this is going to work :(
    }

    public object RunListQuery<T> (List<T> list, SecondaryQuery query) {
        switch (query.QueryType) {
            case LIST_QUERY.LIST:
                return list;
            case LIST_QUERY.SIZE:
                return list.Count;
            case LIST_QUERY.RAND_ITEM:
                return list[Random.Range(0, list.Count)];
        }
        return null;
    }
}
