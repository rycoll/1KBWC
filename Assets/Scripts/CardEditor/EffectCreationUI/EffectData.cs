using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class EffectData {
    public Instruction instruction { get; private set; }
    public string name { get; private set; }
    public string message  { get; private set; }
    public FieldData[] fields { get; private set; }
    public ReturnType returnType { get; private set; }

    public EffectData (Instruction i, string n, string m, FieldData[] f, ReturnType t = ReturnType.NONE) {
        instruction = i;
        name = n;
        message = m;
        returnType = t;
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

    public static Dictionary<Instruction, EffectData> InstructionDataMap = new Dictionary<Instruction, EffectData>{
        {Instruction.IF, new EffectData(
            Instruction.IF,
            "If ... then ...",
            "Run some effects only if a specified condition is met",
            new FieldData[]{ FieldLibrary.ConditionFieldData("Under what condition?") },
            ReturnType.NONE
        )},
        {Instruction.LOOP, new EffectData(
            Instruction.LOOP,
            "Loop",
            "Do some effects a specified number of times",
            new FieldData[] { FieldLibrary.NumberFieldData("How many times?") },
            ReturnType.NONE
        )},
        {Instruction.FOR_LOOP, new EffectData(
            Instruction.FOR_LOOP,
            "For Loop",
            "Do some effects for each item in a list",
            new FieldData[] { FieldLibrary.ListFieldData("What items should be iterated over?") },
            ReturnType.NONE
        )},

        {Instruction.ADD, new EffectData(
            Instruction.ADD,
            "Add",
            "Add two numbers",
            new FieldData[] { 
                FieldLibrary.NumberFieldData("What's the first number to add?"), 
                FieldLibrary.NumberFieldData("What's the second number to add?") 
            },
            ReturnType.NUMBER
        )},
        {Instruction.MULTIPLY, new EffectData(
            Instruction.MULTIPLY,
            "Multiply",
            "Multiply two numbers",
            new FieldData[] { 
                FieldLibrary.NumberFieldData("What's the first  number to multiply?"), 
                FieldLibrary.NumberFieldData("What's the second number to multiply?") 
            },
            ReturnType.NUMBER
        )},
        {Instruction.RANDOM_NUMBER, new EffectData(
            Instruction.RANDOM_NUMBER,
            "Random Number",
            "Get a random number (between 1 and an upper bound)",
            new FieldData[] { FieldLibrary.NumberFieldData("Upper bound") },
            ReturnType.NUMBER
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
            new FieldData[]{ FieldLibrary.PlayerFieldData("Which player?") },
            ReturnType.NUMBER
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
            new FieldData[]{ FieldLibrary.StringFieldData("Name of the counter") },
            ReturnType.NUMBER
        )},

        {Instruction.SET_PLAYER_POINTS, new EffectData(
            Instruction.SET_PLAYER_POINTS,
            "Set player's points",
            "Set the point total of a specified player",
            new FieldData[]{ 
                FieldLibrary.NumberFieldData("New point total"), 
                FieldLibrary.PlayerFieldData("Which player?") 
            }
        )},
        {Instruction.INCREMENT_PLAYER_POINTS, new EffectData(
            Instruction.INCREMENT_PLAYER_POINTS,
            "Increment player's points",
            "Increment the point total of a specified player",
            new FieldData[]{ 
                FieldLibrary.NumberFieldData("How many points?"), 
                FieldLibrary.PlayerFieldData("Which player?") 
            }
        )},
        {Instruction.SET_PLAYER_DRAW, new EffectData(
            Instruction.SET_PLAYER_DRAW,
            "Set player's draw size",
            "Change the number of cards a particular player draws each turn",
            new FieldData[]{ 
                FieldLibrary.NumberFieldData("New number of cards to draw"), 
                FieldLibrary.PlayerFieldData("Which player?") 
            }
        )},
        {Instruction.SET_PLAYER_MAX_HAND, new EffectData(
            Instruction.SET_PLAYER_MAX_HAND,
            "Set player's hand size",
            "Set the maximum hand size for a specified player",
            new FieldData[]{ 
                FieldLibrary.NumberFieldData("New max. hand size"), 
                FieldLibrary.PlayerFieldData("Which player?") 
            }
        )},
        {Instruction.PLAYER_DRAW_CARD, new EffectData(
            Instruction.PLAYER_DRAW_CARD,
            "Player draws card(s)",
            "A specified player draws a specified number of cards",
            new FieldData[]{ 
                FieldLibrary.NumberFieldData("How many cards to draw"), 
                FieldLibrary.PlayerFieldData("Which player?") 
            }
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
    NONE, NUMBER, BOOL, TEXT, CARD, PLAYER, LIST, CONDITION, ROOT_EFFECT
}