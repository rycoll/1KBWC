using System;
using System.Collections.Generic;
using System.Linq;

public static class EffectLibrary {
    public static EffectData[] AllEffectData = new EffectData[] {
        // control effects
        CE_ForLoop.GetEffectData(),
        CE_If.GetEffectData(),
        CE_NumLoop.GetEffectData(),
        // regular effects
        RE_AddToHand.GetEffectData(),
        RE_Choice.GetEffectData(),
        RE_IncrementPlayerPoints.GetEffectData(),
        RE_PlayerDraw.GetEffectData(),
        RE_SetDrawSize.GetEffectData(),
        RE_SetMaxHandSize.GetEffectData(),
        RE_SetPlayerPoints.GetEffectData(),
        RE_SetVariable.GetEffectData(),
    };

    public static EffectData GetEffectDataByName (string name) {
        return AllEffectData.Where((effect, index) => effect.name.Equals(name)).ElementAt(0);
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