using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectFieldContainer {
    public RunTimeValue<GamePlayer> TargetPlayer { get; set; }
    public RunTimeValue<List<GamePlayer>> PlayerList { get; set; }
    public RunTimeValue<Card> TargetCard { get; set; }
    public RunTimeValue<List<Card>> CardList { get; set; }
    public RunTimeValue<Condition> ConditionToEvaluate { get; set; }

    public RunTimeValue<int> NumberToSet { get; set; }
    public RunTimeValue<string> StringToSet { get; set; }
    public RunTimeValue<bool> BoolToSet { get; set; }
    public RunTimeValue<Condition> ConditionToSet { get; set; }

    public RunTimeValue<string> OOEMessage { get; set ;}

    public RunTimeValue<int> Index { get; set; }
    public RunTimeValue<string> LookupString { get; set; }

    public List<FieldEnum> StackOfFieldsToSet = new List<FieldEnum>();

    public enum FieldEnum {
        TargetPlayer, TargetCard, PlayerList, CardList,
        ConditionToEvaluate, ConditionToSet,
        NumberToSet, StringToSet, BoolToSet,
        OOEMessage, Index, LookupString
    }

}