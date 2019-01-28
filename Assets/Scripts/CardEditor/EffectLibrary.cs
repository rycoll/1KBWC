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
    public List<Field> fields;
    public bool takesSubEffects;
    public Type associatedEffect;
}

public enum FieldType {
    NUMBER, ANY_PLAYER, OPPONENT, YOU, CARD, STRING
}

public abstract class Field {

}