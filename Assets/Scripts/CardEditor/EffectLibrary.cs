using System;
using System.Collections.Generic;
using System.Linq;

public static class EffectLibrary {
    private static EffectData[] AllEffectData;
    public static EffectData[] GetAllEffectData () {
        if (AllEffectData == null) {
            AllEffectData = new EffectData[] {
                // control effects
                CE_ForLoop_Card.GetEffectData(),
                CE_If.GetEffectData(),
                CE_NumLoop.GetEffectData(),
                // regular effects
                RE_AddToHand.GetEffectData(),
                RE_Choice_Card.GetEffectData(),
                RE_IncrementPlayerPoints.GetEffectData(),
                RE_PlayerDraw.GetEffectData(),
                RE_SetDrawSize.GetEffectData(),
                RE_SetMaxHandSize.GetEffectData(),
                RE_SetPlayerPoints.GetEffectData(),
                RE_SetVariable.GetEffectData(),
            };
        }
        return AllEffectData;
    }

    public static EffectData GetEffectDataByName (string name) {
        return GetAllEffectData().Where((effect, index) => effect.name.Equals(name)).ElementAt(0);
    }
}

public class EffectData {
    public string name;
    public string desc;
    public List<FieldData> fields;
    public bool takesSubEffects;
    public CardEffect effect;
}
