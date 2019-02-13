using System;
using System.Collections.Generic;
using System.Linq;

public static class FieldLibrary {

    public static FieldData BoolFieldData = new FieldData() {
        text = "Yes or No?",
        enterValue = EnterValueType.BOOL,
        queryDropdown = new List<QueryData>{
            Q_Flag.QueryData
        }
    };

    public static FieldData CardFieldData = new FieldData() {
        text = "Which card?",
        enterValue = EnterValueType.NONE,
        queryDropdown = new List<QueryData>()
    };

    public static FieldData NumberFieldData = new FieldData() {
        text = "What number?",
        enterValue = EnterValueType.NUMBER,
        queryDropdown = new List<QueryData> {
            Q_Counter.QueryData,
            Q_DeckSize.QueryData,
            Q_DiscardSize.QueryData,
            Q_PlayerDrawSize.QueryData,
            Q_PlayerPoints.QueryData,
            Q_TableSize.QueryData
        }
    };

    public static FieldData PlayerFieldData = new FieldData() {
        text = "Which player?",
        enterValue = EnterValueType.NONE,
        queryDropdown = new List<QueryData>{
            Q_Variable.QueryData
        }
    };

    public static FieldData StringFieldData = new FieldData() {
        text = "Text",
        enterValue = EnterValueType.TEXT,
        queryDropdown = new List<QueryData> {
            Q_PlayerName.QueryData,
            Q_Variable.QueryData
        }
    };

    public static FieldData ListFieldData = new FieldData() {
        text = "List",
        enterValue = EnterValueType.NONE,
        queryDropdown = new List<QueryData> {
            Q_DeckCards.QueryData,
            Q_DiscardCards.QueryData,
            Q_Opponents.QueryData,
            Q_Players.QueryData,
            Q_TableCards.QueryData
        }
    };
}


public struct FieldData {
    public string text;
    public EnterValueType enterValue;

    public List<QueryData> queryDropdown;
}

public enum EnterValueType {
    NONE, TEXT, NUMBER, BOOL
}

