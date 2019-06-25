using System.Collections.Generic;

public static class FieldLibrary {

    public static FieldData BoolFieldData = new FieldData() {
        text = "Yes or No?",
        enterValue = EnterValueType.BOOL,
        returnType = ReturnType.BOOL
    };

    public static FieldData CardFieldData = new FieldData() {
        text = "Which card?",
        enterValue = EnterValueType.NONE,
        returnType = ReturnType.CARD
    };

    public static FieldData NumberFieldData = new FieldData() {
        text = "What number?",
        enterValue = EnterValueType.NUMBER,
        returnType = ReturnType.NUMBER
    };

    public static FieldData PlayerFieldData = new FieldData() {
        text = "Which player?",
        enterValue = EnterValueType.NONE,
        returnType = ReturnType.PLAYER
    };

    public static FieldData StringFieldData = new FieldData () {
        text = "Text",
        enterValue = EnterValueType.TEXT,
        returnType = ReturnType.TEXT
    };

    public static FieldData ListFieldData = new FieldData() {
        text = "List",
        enterValue = EnterValueType.NONE,
        returnType = ReturnType.LIST
    };

    public static FieldData ConditionFieldData = new FieldData() {

    };

    
}

public class FieldData {
    public string text;
    public EnterValueType enterValue;
    public ReturnType returnType;

    public List<EffectData> GetDropdownValues() {
        switch(returnType) {
            case ReturnType.BOOL:
                return EffectData.GetAllBoolReturningEffects();
            case ReturnType.CARD:
                return EffectData.GetAllCardReturningEffects();
            case ReturnType.PLAYER:
                return EffectData.GetAllPlayerReturningEffects();
            case ReturnType.NUMBER:
                return EffectData.GetAllNumberReturningEffects();
            case ReturnType.TEXT:
                return EffectData.GetAllTextReturningEffects();
            case ReturnType.NONE:
            default:
                return new List<EffectData>();
        }
    }
}

public enum EnterValueType {
    NONE, TEXT, NUMBER, BOOL
}

