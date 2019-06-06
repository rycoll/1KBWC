using System;
using System.Collections.Generic;
using System.Linq;

public static class EffectLibrary {
    private static EffectData[] AllEffectData;
    public static EffectData[] GetAllEffectData () {
        if (AllEffectData == null) {
            AllEffectData = new EffectData[] {
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
}
