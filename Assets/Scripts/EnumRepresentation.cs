using System;

public class EnumRepesentation {
    
    private string[] values;
    private Instruction instruction;

    public EnumRepesentation (string[] arr, Instruction i) {
        values = arr;
        instruction = i;
    }

    public string getName (int n) {
        return values[n];
    }

    public string[] getNames () {
        return values;
    }

    public int getIndex (string value) {
        return Array.IndexOf(values, value);
    }

    public Instruction getInstruction () {
        return instruction;
    }
    
    public static EnumRepesentation EnumLookup (string lookupKey) {
        switch (lookupKey) {
            case "ENUM_CONDITION_OPERATOR":
                return new EnumRepesentation(Enum.GetNames(typeof(ConditionOperator)), Instruction.ENUM_CONDITION_OPERATOR);
            case "ENUM_CONDITION_TYPE":
                return new EnumRepesentation(Enum.GetNames(typeof(ConditionType)), Instruction.ENUM_CONDITION_TYPE);
            case "ENUM_DECK_POSITION":
                return new EnumRepesentation(Enum.GetNames(typeof(DeckLocation)), Instruction.ENUM_DECK_POSITION);
            case "ENUM_LIST_TYPE":
                return new EnumRepesentation(Enum.GetNames(typeof(ListType)), Instruction.ENUM_LIST_TYPE);
            default:
                return new EnumRepesentation(new string[0], Instruction.ERROR);
        }
    }
}