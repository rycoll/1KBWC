using System;
using System.Collections.Generic;
using System.Linq;

public static class FieldLibrary {

    private static FieldData BoolFieldData = new FieldData() {
        text = "Yes or No?",
        enterValue = EnterValueType.BOOL,
    }; 
    public static FieldData GetBoolFieldData () {
        if (BoolFieldData.queryDropdown == null) {
            BoolFieldData.queryDropdown = new List<QueryData>();
            BoolFieldData.queryDropdown.Add(Q_Flag.GetQueryData());
        }
        return BoolFieldData;
    } 

    private static FieldData CardFieldData = new FieldData() {
        text = "Which card?",
        enterValue = EnterValueType.NONE,
        queryDropdown = new List<QueryData>()
    };
    public static FieldData GetCardFieldData () {
        return CardFieldData;
    }

    private static FieldData NumberFieldData = new FieldData() {
        text = "What number?",
        enterValue = EnterValueType.NUMBER,
    };
    public static FieldData GetNumberFieldData () {
        if (NumberFieldData.queryDropdown == null) {
            NumberFieldData.queryDropdown = new List<QueryData>();
            NumberFieldData.queryDropdown.Add(Q_Counter.GetQueryData());
            NumberFieldData.queryDropdown.Add(Q_DeckSize.GetQueryData());
            NumberFieldData.queryDropdown.Add(Q_DiscardSize.GetQueryData());
            NumberFieldData.queryDropdown.Add(Q_PlayerDrawSize.GetQueryData());
            NumberFieldData.queryDropdown.Add(Q_PlayerPoints.GetQueryData());
            NumberFieldData.queryDropdown.Add(Q_TableSize.GetQueryData());
        }
        return NumberFieldData;
    }

    private static FieldData PlayerFieldData = new FieldData() {
        text = "Which player?",
        enterValue = EnterValueType.NONE,
    };
    public static FieldData GetPlayerFieldData () {
        if (PlayerFieldData.queryDropdown == null) {
            PlayerFieldData.queryDropdown = new List<QueryData>();
            PlayerFieldData.queryDropdown.Add(Q_ActivePlayer.GetQueryData());
        }
        return PlayerFieldData;
    }

    private static FieldData StringFieldData = new FieldData () {
            text = "Text",
            enterValue = EnterValueType.TEXT,
        };
    public static FieldData GetStringFieldData () {
        if (StringFieldData.queryDropdown == null) {
            StringFieldData.queryDropdown = new List<QueryData>();
            StringFieldData.queryDropdown.Add(Q_PlayerName.GetQueryData());
            StringFieldData.queryDropdown.Add(Q_Variable.GetQueryData());
        }
        return StringFieldData;
    }


    private static FieldData ListFieldData = new FieldData() {
        text = "List",
        enterValue = EnterValueType.NONE,
    };
    public static FieldData GetListFieldData () {
        if (ListFieldData.queryDropdown == null) {
            ListFieldData.queryDropdown = new List<QueryData>();
            ListFieldData.queryDropdown.Add(Q_DeckCards.GetQueryData());
            ListFieldData.queryDropdown.Add(Q_DiscardCards.GetQueryData());
            ListFieldData.queryDropdown.Add(Q_Opponents.GetQueryData());
            ListFieldData.queryDropdown.Add(Q_Players.GetQueryData());
            ListFieldData.queryDropdown.Add(Q_TableCards.GetQueryData());
        }
        return ListFieldData;
    }
}


public class FieldData {
    public string text;
    public EnterValueType enterValue;
    public List<QueryData> queryDropdown;
}

public enum EnterValueType {
    NONE, TEXT, NUMBER, BOOL
}

