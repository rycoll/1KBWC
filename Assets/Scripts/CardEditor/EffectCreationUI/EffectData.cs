using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

[System.Serializable]
public class EffectData {
    public Instruction instruction { get; private set; }
    public string name { get; private set; }
    public string type { get; private set; }
    public string message  { get; private set; }
    public FieldData[] fields { get; private set; }
    public ReturnType returnType { get; private set; }
    public bool canBeRoot { get; private set; }

    public static List<EffectData> Effects = EffectJSONParser.GetEffectDataFromJSONFile();

    public EffectData (Instruction i, string n, string t, string m, FieldData[] f, bool root, ReturnType rt = ReturnType.NONE) {
        instruction = i;
        name = n;
        type = t;
        message = m;
        returnType = rt;
        fields = f;
        canBeRoot = root;
    }

    public static EffectData GetEffectDataByName (string n) {
        try {
            if (n == "ENDLOOP") {
                Debug.Log("endloop");
            }
            return Effects.First(value => value.name.ToLower() == n.ToLower());
        } catch (InvalidOperationException e) {
            // nothing found
            Debug.LogWarning("Couldn't find an effect named '" + n.ToLower() + "'. " + e);
            return null;
        }
    }

    public static List<EffectData> GetAllRootEffects () {
        return Effects.Where(effect => effect.canBeRoot).ToList();
    }

}

