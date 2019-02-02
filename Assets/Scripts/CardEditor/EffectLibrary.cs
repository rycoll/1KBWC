using System;
using System.Collections.Generic;

public static class EffectLibrary {
    public static EffectData[] GetEffects () {

        return new EffectData[] {
        
        };
    }
}

public struct EffectData {
    public string name;
    public string desc;
    public List<FieldType> fields;
    public bool takesSubEffects;

}

public enum FieldType {
    NUMBER, PLAYER, CARD, STRING, CONDITION, LIST
}