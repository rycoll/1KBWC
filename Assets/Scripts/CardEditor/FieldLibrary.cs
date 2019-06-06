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
        }
        return BoolFieldData;
    } 

    private static FieldData CardFieldData = new FieldData() {
        text = "Which card?",
        enterValue = EnterValueType.NONE
    };
    public static FieldData GetCardFieldData () {
        if (CardFieldData.queryDropdown == null) {
            CardFieldData.queryDropdown = new List<QueryData>();
        }
        return CardFieldData;
    }

    private static FieldData NumberFieldData = new FieldData() {
        text = "What number?",
        enterValue = EnterValueType.NUMBER,
    };
    public static FieldData GetNumberFieldData () {
        if (NumberFieldData.queryDropdown == null) {
            NumberFieldData.queryDropdown = new List<QueryData>();
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

