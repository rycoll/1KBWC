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

    public EffectData (Instruction i, string n, string m, FieldData[] f, bool root, ReturnType t = ReturnType.NONE) {
        instruction = i;
        name = n;
        message = m;
        returnType = t;
        fields = f;
        canBeRoot = root;
    }

    public static Instruction[] BaseInstructions = new Instruction[]{
        Instruction.IF,
        Instruction.LOOP,
        Instruction.FOR_LOOP,

        Instruction.SET_PLAYER_POINTS,
        Instruction.INCREMENT_PLAYER_POINTS,
        Instruction.SET_PLAYER_DRAW,
        Instruction.SET_PLAYER_MAX_HAND,
        Instruction.PLAYER_DRAW_CARD,
        Instruction.SET_COUNTER,
        Instruction.MOVE_TO_DECK,
        Instruction.MOVE_TO_DISCARD,
    };

    public static EffectData GetEffectDataByName (string n) {
        try {
            return InstructionDataMap.Values.First(value => value.name == n);
        } catch (InvalidOperationException e) {
            // nothing found
            Debug.LogWarning("Couldn't find an effect named '" + n + "'. " + e);
            return null;
        }
    }

    public static List<EffectData> GetAllRootEffects () {
        return InstructionDataMap.Values
            .Where(value => BaseInstructions.Contains(value.instruction))
            .ToList();
    }

    public static List<EffectData> GetAllBoolReturningEffects () {
        return InstructionDataMap.Values.Where(value => value.returnType == ReturnType.BOOL).ToList();
    }

    public static List<EffectData> GetAllNumberReturningEffects () {
        return InstructionDataMap.Values.Where(value => value.returnType == ReturnType.NUMBER).ToList();
    }

    public static List<EffectData> GetAllTextReturningEffects () {
        return InstructionDataMap.Values.Where(value => value.returnType == ReturnType.TEXT).ToList();
    }

    public static List<EffectData> GetAllCardReturningEffects () {
        return InstructionDataMap.Values.Where(value => value.returnType == ReturnType.CARD).ToList();
    }

    public static List<EffectData> GetAllPlayerReturningEffects () {
        return InstructionDataMap.Values.Where(value => value.returnType == ReturnType.PLAYER).ToList();
    }

    public static Dictionary<Instruction, EffectData> InstructionDataMap = new Dictionary<Instruction, EffectData>();


}

public enum ReturnType {
    NONE, NUMBER, BOOL, TEXT, CARD, PLAYER, LIST, CONDITION, ROOT_EFFECT,
    ENUM_CONDITION_OPERATOR, ENUM_DECK_POSITION
}