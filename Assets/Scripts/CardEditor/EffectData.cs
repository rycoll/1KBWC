using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class EffectData {
    public Instruction instruction { get; private set; }
    public bool takesSubEffects  { get; private set; }
    public string name { get; private set; }
    public string message  { get; private set; }
    public FieldData[] fields { get; private set; }
    public ReturnType returnType { get; private set; }

    public EffectData (Instruction i, string n, string m, FieldData[] f, ReturnType t = ReturnType.NONE, bool b = false) {
        instruction = i;
        takesSubEffects = b;
        name = n;
        message = m;
        fields = f;
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

    public static Dictionary<Instruction, EffectData> InstructionDataMap = new Dictionary<Instruction, EffectData>{
        {Instruction.IF, new EffectData(
            Instruction.IF,
            "If ... then ...",
            "Run some effects only if a specified condition is met",
            new FieldData[]{ FieldLibrary.ConditionFieldData },
            ReturnType.NONE,
            true
        )},
        {Instruction.LOOP, new EffectData(
            Instruction.LOOP,
            "Loop",
            "Do some effects a specified number of times",
            new FieldData[] { FieldLibrary.NumberFieldData },
            ReturnType.NONE,
            true
        )},
        {Instruction.FOR_LOOP, new EffectData(
            Instruction.FOR_LOOP,
            "For Loop",
            "Do some effects for each item in a list",
            new FieldData[] { FieldLibrary.ListFieldData },
            ReturnType.NONE,
            true
        )},

         {Instruction.GET_ACTIVE_PLAYER, new EffectData(
            Instruction.GET_ACTIVE_PLAYER,
            "Active Player",
            "The player whose turn it is",
            new FieldData[0],
            ReturnType.PLAYER
        )},
        /* {Instruction.GET_PLAYER, new EffectData(
            Instruction.GET_PLAYER,
            "Active Player",
            "The player whose turn it is",
            new FieldData[0],
            ReturnType.PLAYER
        )}, */
        {Instruction.GET_PLAYER_POINTS, new EffectData(
            Instruction.GET_PLAYER_POINTS,
            "Player's points",
            "The point total of a specified player",
            new FieldData[]{ FieldLibrary.PlayerFieldData },
            ReturnType.PLAYER
        )},
        {Instruction.TARGET_PLAYER, new EffectData(
            Instruction.TARGET_PLAYER,
            "Choose a player",
            "The current player chooses a player",
            new FieldData[0],
            ReturnType.PLAYER
        )},
        {Instruction.TARGET_CARD, new EffectData(
            Instruction.TARGET_CARD,
            "Choose a card",
            "The current player chooses a card in play",
            new FieldData[0],
            ReturnType.CARD
        )},
        {Instruction.READ_COUNTER, new EffectData(
            Instruction.READ_COUNTER,
            "Read counter",
            "Read the value of a counter (enter name)",
            new FieldData[]{ FieldLibrary.StringFieldData },
            ReturnType.NUMBER
        )},

        {Instruction.SET_PLAYER_POINTS, new EffectData(
            Instruction.SET_PLAYER_POINTS,
            "Set player's points",
            "Set the point total of a specified player",
            new FieldData[]{ FieldLibrary.NumberFieldData, FieldLibrary.PlayerFieldData }
        )},
        {Instruction.INCREMENT_PLAYER_POINTS, new EffectData(
            Instruction.INCREMENT_PLAYER_POINTS,
            "Increment player's points",
            "Increment the point total of a specified player",
            new FieldData[]{ FieldLibrary.NumberFieldData, FieldLibrary.PlayerFieldData }
        )},
        {Instruction.SET_PLAYER_DRAW, new EffectData(
            Instruction.SET_PLAYER_DRAW,
            "Set player's draw size",
            "Change the number of cards a particular player draws each turn",
            new FieldData[]{ FieldLibrary.NumberFieldData, FieldLibrary.PlayerFieldData }
        )},
        {Instruction.SET_PLAYER_MAX_HAND, new EffectData(
            Instruction.SET_PLAYER_MAX_HAND,
            "Set player's hand size",
            "Set the maximum hand size for a specified player",
            new FieldData[]{ FieldLibrary.NumberFieldData, FieldLibrary.PlayerFieldData }
        )},
        {Instruction.PLAYER_DRAW_CARD, new EffectData(
            Instruction.PLAYER_DRAW_CARD,
            "Player draws card(s)",
            "A specified player draws a specified number of cards",
            new FieldData[]{ FieldLibrary.NumberFieldData, FieldLibrary.PlayerFieldData }
        )},
        // {Instruction.SET_COUNTER, new Instruction[]{
        //     Instruction.STRING,
        //     Instruction.INT
        // }},
        // {Instruction.MOVE_TO_DISCARD, new Instruction[]{
        //     Instruction.CARD,
        //     // byte instruction
        // }},
        // {Instruction.MOVE_TO_DECK, new Instruction[]{
        //     Instruction.CARD,
        // }},

        // {Instruction.ERROR, }
    };

    
}

public enum ReturnType {
    NONE, NUMBER, BOOL, TEXT, CARD, PLAYER, LIST
}