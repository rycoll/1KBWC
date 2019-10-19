using System.Collections.Generic;

public class FieldData {
    public string text;
    public EnterValueType enterValue;
    public ReturnType returnType;
}

public enum EnterValueType {
    NONE, TEXT, NUMBER, BOOL
}

