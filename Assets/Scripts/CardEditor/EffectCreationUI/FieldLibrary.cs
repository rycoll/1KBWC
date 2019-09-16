using System.Collections.Generic;

public static class FieldLibrary {

    public static FieldData BoolFieldData (string t) {
        return new FieldData() {
            text = t,
            enterValue = EnterValueType.BOOL,
            returnType = ReturnType.BOOL
        };
    }

    public static FieldData CardFieldData (string t) {
        return new FieldData() {
            text = t,
            enterValue = EnterValueType.NONE,
            returnType = ReturnType.CARD
        };
    }

    public static FieldData NumberFieldData (string t) {
        return new FieldData() {
            text = t,
            enterValue = EnterValueType.NUMBER,
            returnType = ReturnType.NUMBER
        };
    }

    public static FieldData PlayerFieldData (string t) {
        return new FieldData() {
            text = t,
            enterValue = EnterValueType.NONE,
            returnType = ReturnType.PLAYER
        };
    }

    public static FieldData StringFieldData (string t) {
        return new FieldData() {
            text = t,
            enterValue = EnterValueType.TEXT,
            returnType = ReturnType.TEXT
        };
    }

    public static FieldData ListFieldData (string t) {
        return new FieldData() {
            text = t,
            enterValue = EnterValueType.NONE,
            returnType = ReturnType.LIST
        };
    }

    public static FieldData ConditionFieldData (string t) {
        return new FieldData() {
        };
    }

    public static FieldData RootEffectFieldData (string t) {
        return new FieldData() {
            text = t,
            enterValue = EnterValueType.NONE,
            returnType = ReturnType.ROOT_EFFECT
        };
    }

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
            case ReturnType.ROOT_EFFECT:
                return EffectData.GetAllRootEffects();
            case ReturnType.NONE:
            default:
                return new List<EffectData>();
        }
    }
}

public enum EnterValueType {
    NONE, TEXT, NUMBER, BOOL
}

