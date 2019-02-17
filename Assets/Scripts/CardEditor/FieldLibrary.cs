using System;
using System.Collections.Generic;
using System.Linq;

public static class FieldLibrary {

    private static FieldData BoolFieldData;
    public static FieldData GetBoolFieldData () {
        if (BoolFieldData == null) {
            BoolFieldData = new FieldData() {
                text = "Yes or No?",
                enterValue = EnterValueType.BOOL,
                queryDropdown = new List<QueryData>{
                    Q_Flag.GetQueryData()
                }
            };   
        }
        return BoolFieldData;
    } 

    private static FieldData CardFieldData;
    public static FieldData GetCardFieldData () {
        if (CardFieldData == null) {
            CardFieldData = new FieldData() {
                text = "Which card?",
                enterValue = EnterValueType.NONE,
                queryDropdown = new List<QueryData>()
            };
        }
        return CardFieldData;
    }

    private static FieldData NumberFieldData;
    public static FieldData GetNumberFieldData () {
        if (NumberFieldData == null) {
            NumberFieldData = new FieldData() {
                text = "What number?",
                enterValue = EnterValueType.NUMBER,
                queryDropdown = new List<QueryData> {
                    Q_Counter.GetQueryData(),
                    Q_DeckSize.GetQueryData(),
                    Q_DiscardSize.GetQueryData(),
                    Q_PlayerDrawSize.GetQueryData(),
                    Q_PlayerPoints.GetQueryData(),
                    Q_TableSize.GetQueryData()
                }
            };
        }
        return NumberFieldData;
    }

    private static FieldData PlayerFieldData;
    public static FieldData GetPlayerFieldData () {
        if (PlayerFieldData == null) {
            PlayerFieldData = new FieldData() {
                text = "Which player?",
                enterValue = EnterValueType.NONE,
                queryDropdown = new List<QueryData>{
                }
            };
        }
        return PlayerFieldData;
    }

    private static FieldData StringFieldData;
    public static FieldData GetStringFieldData () {
        if (StringFieldData == null) {
            StringFieldData = new FieldData () {
                text = "Text",
                enterValue = EnterValueType.TEXT,
                queryDropdown = new List<QueryData> {
                    Q_PlayerName.GetQueryData(),
                    Q_Variable.GetQueryData()
                }
            };
        }
        return StringFieldData;
    }


    private static FieldData ListFieldData;
    public static FieldData GetListFieldData () {
        if (ListFieldData == null) {
            ListFieldData = new FieldData() {
                text = "List",
                enterValue = EnterValueType.NONE,
                queryDropdown = new List<QueryData> {
                    Q_DeckCards.GetQueryData(),
                    Q_DiscardCards.GetQueryData(),
                    Q_Opponents.GetQueryData(),
                    Q_Players.GetQueryData(),
                    Q_TableCards.GetQueryData()
                }
            };
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

