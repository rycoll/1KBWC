using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

[System.Serializable]
public class EffectData {
    public Instruction instruction { get; private set; }
    public string name { get; private set; }
    public string message  { get; private set; }
    public FieldData[] fields { get; private set; }
    public ReturnType returnType { get; private set; }
    public bool canBeRoot { get; private set; }

    public static List<EffectData> Effects = EffectJSONParser.GetEffectDataFromJSONFile();

    public EffectData (Instruction i, string n, string m, FieldData[] f, bool root, ReturnType t = ReturnType.NONE) {
        instruction = i;
        name = n;
        message = m;
        returnType = t;
        fields = f;
        canBeRoot = root;
    }

    public static EffectData GetEffectDataByName (string n) {
        try {
            return Effects.First(value => value.name == n);
        } catch (InvalidOperationException e) {
            // nothing found
            Debug.LogWarning("Couldn't find an effect named '" + n + "'. " + e);
            return null;
        }
    }

    public static List<EffectData> GetAllRootEffects () {
        return Effects.Where(effect => effect.canBeRoot).ToList();
    }

}

public enum ReturnType {
    NONE, NUMBER, BOOL, TEXT, CARD, PLAYER, LIST, CONDITION, ROOT_EFFECT,
    ENUM_CONDITION_OPERATOR, ENUM_DECK_POSITION
}