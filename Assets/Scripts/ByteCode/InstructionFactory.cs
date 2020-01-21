using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

/* These methods help ensure that each instruction type is built consistently! */

public class InstructionFactory {

    private static System.Random rng = new System.Random();
    public static int RandomID {
        get {
            return rng.Next(1, Int32.MaxValue);
        }
    }

    #region GETTERS 

    public static List<byte> Make_Add(List<byte> a, List<byte> b) {
        List<byte> bytes = new List<byte>(a);
        bytes.AddRange(new List<byte>(b));
        bytes.Add((byte) Instruction.ADD);
        return bytes;
    }

    public static List<byte> Make_IsTrue(List<byte> boolean) {
        List<byte> bytes = new List<byte>();
        bytes.AddRange(LiteralFactory.CreateBoolLiteral(true));
        bytes.AddRange(LiteralFactory.CreateEnumLiteral((byte) ConditionOperator.EQUAL, Instruction.ENUM_CONDITION_OPERATOR));
        bytes.AddRange(new List<byte>(boolean));
        bytes.Add((byte) Instruction.BOOL_COMPARISON);
        return bytes;
    }

    public static List<byte> Make_IsFalse(List<byte> boolean) {
        List<byte> bytes = new List<byte>();
        bytes.AddRange(LiteralFactory.CreateBoolLiteral(false));
        bytes.AddRange(LiteralFactory.CreateEnumLiteral((byte) ConditionOperator.EQUAL, Instruction.ENUM_CONDITION_OPERATOR));
        bytes.AddRange(new List<byte>(boolean));
        bytes.Add((byte) Instruction.BOOL_COMPARISON);
        return bytes;
    }

    public static List<byte> Make_Multiply(List<byte> a, List<byte> b) {
        List<byte> bytes = new List<byte>(a);
        bytes.AddRange(new List<byte>(b));
        bytes.Add((byte) Instruction.MULTIPLY);
        return bytes;
    }

    public static List<byte> Make_RandomNumber(List<byte> upper) {
        List<byte> bytes = new List<byte>(upper);
        bytes.Add((byte) Instruction.RANDOM_NUMBER);
        return bytes;
    }

    public static List<byte> Make_ListLength(List<byte> list) {
        List<byte> bytes = new List<byte>(list);
        bytes.Add((byte) Instruction.LIST_LENGTH);
        return bytes;
    }

    public static List<byte> Make_GetPlayer (List<byte> id) {
        List<byte> bytes = new List<byte>(id);
        bytes.Add((byte) Instruction.GET_PLAYER);
        return bytes;
    }

    public static List<byte> Make_GetPlayerPoints (List<byte> id) {
        List<byte> bytes = new List<byte>(id);
        bytes.Add((byte) Instruction.GET_PLAYER_POINTS);
        return bytes;
    }

    public static List<byte> Make_GetCardsInHand (List<byte> id) {
        List<byte> bytes = new List<byte>(id);
        bytes.Add((byte) Instruction.GET_CARDS_IN_HAND);
        return bytes;
    }   

    public static List<byte> Make_BoolComparison(List<byte> operandA, List<byte> operandB, byte operatorEnum) {
        List<byte> bytes = new List<byte>();
        bytes.AddRange(new List<byte>(operandB));
        bytes.AddRange(LiteralFactory.CreateEnumLiteral(operatorEnum, Instruction.ENUM_CONDITION_OPERATOR));
        bytes.AddRange(new List<byte>(operandA));
        bytes.Add((byte) Instruction.BOOL_COMPARISON);
        return bytes;
    }

    public static List<byte> Make_NumComparison(List<byte> operandA, List<byte> operandB, byte operatorEnum) {
        List<byte> bytes = new List<byte>();
        bytes.AddRange(new List<byte>(operandB));
        bytes.AddRange(LiteralFactory.CreateEnumLiteral(operatorEnum, Instruction.ENUM_CONDITION_OPERATOR));
        bytes.AddRange(new List<byte>(operandA));
        bytes.Add((byte) Instruction.NUM_COMPARISON);
        return bytes;
    }

    public static List<byte> Make_ReadCounter (List<byte> str) {
        List<byte> bytes = new List<byte>(str);
        bytes.Add((byte) Instruction.READ_COUNTER);
        return bytes;
    }

    public static List<byte> Make_CardHasTag (List<byte> card, List<byte> str) {
        List<byte> bytes = new List<byte>(str);
        bytes.AddRange(new List<byte>(card));
        bytes.Add((byte) Instruction.CARD_HAS_TAG);
        return bytes;
    }

    public static List<byte> Make_PlayerIsWinning (List<byte> player) {
        List<byte> bytes = new List<byte>(player);
        bytes.Add((byte) Instruction.PLAYER_IS_WINNING);
        return bytes;
    }

    public static List<byte> Make_PlayerIsLosing (List<byte> player) {
        List<byte> bytes = new List<byte>(player);
        bytes.Add((byte) Instruction.PLAYER_IS_LOSING);
        return bytes;
    }

    #endregion

    #region CONTROL

    public static List<byte> Make_EndIf(int id) {
        List<byte> idArr = LiteralFactory.CreateIntLiteral(id);
        List<byte> bytes = new List<byte>();
        bytes.AddRange(new List<byte>(idArr));
        bytes.Add((byte) Instruction.ENDIF);
        return bytes;
    }

    public static List<byte> Make_If(List<byte> code, List<byte> condition) {
        int id = RandomID;
        List<byte> endif = Make_EndIf(id);
        List<byte> bytes = new List<byte>();
        bytes.AddRange(endif);
        bytes.AddRange(new List<byte>(code));
        bytes.AddRange(new List<byte>(condition));
        bytes.AddRange(new List<byte>(
            LiteralFactory.CreateIntLiteral(id)
        ));
        bytes.Add((byte) Instruction.IF);
        return bytes;
    }

    public static List<byte> Make_Unless(List<byte> code, List<byte> condition) {
        int id = RandomID;
        List<byte> endif = Make_EndIf(id);
        List<byte> bytes = new List<byte>();
        bytes.AddRange(endif);
        bytes.AddRange(new List<byte>(code));
        bytes.AddRange(new List<byte>(condition));
        bytes.AddRange(new List<byte>(
            LiteralFactory.CreateIntLiteral(id)
        ));
        bytes.Add((byte) Instruction.UNLESS);
        return bytes;
    }

    public static List<byte> Make_AddToRegister(List<byte> id, List<byte> code) {
        List<byte> bytes = new List<byte>();
        bytes.AddRange(code);
        bytes.AddRange(LiteralFactory.CreateIntLiteral(code.Count));
        bytes.AddRange(id);
        bytes.Add((byte) Instruction.ADD_TO_REGISTER);
        return bytes;
    }

    public static List<byte> Make_EndLoop(int id) {
        List<byte> idArr = LiteralFactory.CreateIntLiteral(id);
        List<byte> bytes = new List<byte>();
        bytes.AddRange(new List<byte>(idArr));
        bytes.Add((byte) Instruction.ENDLOOP);
        return bytes;
    }

    public static List<byte> Make_Loop (List<byte> num, List<byte> code) {
        int id = RandomID;
        List<byte> endloop = Make_EndLoop(id);
        List<byte> bytes = new List<byte>();
        bytes.AddRange(endloop);
        bytes.AddRange(new List<byte>(code));
        bytes.AddRange(new List<byte>(num));
        bytes.AddRange(new List<byte>(
            LiteralFactory.CreateIntLiteral(id)
        ));
        bytes.Add((byte) Instruction.LOOP);
        return bytes;
    }

    public static List<byte> Make_ForLoop (List<byte> itemList, List<byte> code) {
        int id = RandomID;
        List<byte> endloop = Make_EndLoop(id);
        List<byte> bytes = new List<byte>();
        bytes.AddRange(endloop);
        bytes.AddRange(new List<byte>(code));
        bytes.AddRange(new List<byte>(itemList));
        bytes.AddRange(new List<byte>(
            LiteralFactory.CreateIntLiteral(id)
        ));
        bytes.Add((byte) Instruction.FOR_LOOP);
        return bytes;
    }

    public static List<byte> Make_ForLoop (List<byte> itemList, List<byte> code, int id) {
        List<byte> endloop = Make_EndLoop(id);
        List<byte> bytes = new List<byte>();
        bytes.AddRange(endloop);
        bytes.AddRange(new List<byte>(code));
        bytes.AddRange(new List<byte>(itemList));
        bytes.AddRange(new List<byte>(
            LiteralFactory.CreateIntLiteral(id)
        ));
        bytes.Add((byte) Instruction.FOR_LOOP);
        return bytes;
    }

    #endregion

    #region SETTERS

    public static List<byte> Make_PlayerDrawCards (List<byte> player, List<byte> num) {
        List<byte> bytes = new List<byte>(new List<byte>(num));
        bytes.AddRange(new List<byte>(player));
        bytes.Add((byte) Instruction.PLAYER_DRAW_CARD);
        return bytes;
    }

    public static List<byte> Make_SetCounter (List<byte> str, List<byte> num) {
        List<byte> bytes = new List<byte>(new List<byte>(num));
        bytes.AddRange(new List<byte>(str));
        bytes.Add((byte) Instruction.SET_COUNTER);
        return bytes;
    }

    public static List<byte> Make_SetPlayerDraw (List<byte> player, List<byte> num) {
        List<byte> bytes = new List<byte>(new List<byte>(num));
        bytes.AddRange(new List<byte>(player));
        bytes.Add((byte) Instruction.SET_PLAYER_DRAW);
        return bytes;
    }

    public static List<byte> Make_SetPlayerMaxHand (List<byte> player, List<byte> num) {
        List<byte> bytes = new List<byte>(new List<byte>(num));
        bytes.AddRange(new List<byte>(player));
        bytes.Add((byte) Instruction.SET_PLAYER_MAX_HAND);
        return bytes;
    }

    public static List<byte> Make_SetPlayerPoints (List<byte> player, List<byte> num) {
        List<byte> bytes = new List<byte>(new List<byte>(num));
        bytes.AddRange(new List<byte>(player));
        bytes.Add((byte) Instruction.SET_PLAYER_POINTS);
        return bytes;
    }

    public static List<byte> Make_IncrementPlayerPoints(List<byte> player, List<byte> num) {
        List<byte> bytes = new List<byte>(new List<byte>(num));
        bytes.AddRange(new List<byte>(player));
        bytes.Add((byte) Instruction.INCREMENT_PLAYER_POINTS);
        return bytes;
    }

    public static List<byte> Make_MoveToDeck(List<byte> card, List<byte> location) {
        List<byte> bytes = new List<byte>(
            new List<byte>(
                LiteralFactory.CreateEnumLiteral(location[0], Instruction.ENUM_DECK_POSITION)
            )
        );
        bytes.AddRange(new List<byte>(card));
        bytes.Add((byte) Instruction.MOVE_TO_DECK);
        return bytes;
    }

    public static List<byte> Make_MoveToDiscard(List<byte> card) {
        List<byte> bytes = new List<byte>(card);
        bytes.Add((byte) Instruction.MOVE_TO_DISCARD);
        return bytes;
    }

    public static List<byte> Make_SingleByteInstruction(Instruction instruction) {
        return new List<byte>{(byte) instruction};
    }

    #endregion

     public static List<byte> RunInstructionFactoryForNode (EffectBuilderItem node) {

         if (node.enteredValue != null && node.enteredValue.Count > 0) {
            return node.enteredValue;
         }

        node.children.Reverse();
        List<List<byte>> childInstructions = node.children.Select(childNode => {
            return RunInstructionFactoryForNode(childNode);
        }).ToList();

        Debug.Log($"Child instructions for {node.effectData.instruction}: {childInstructions.Count}");

        try {

            switch(node.effectData.instruction) {
                // FUNCTIONS
                case Instruction.RANDOM_NUMBER:
                    return Make_RandomNumber(childInstructions[0]);
                case Instruction.ADD:
                    return Make_Add(childInstructions[0], childInstructions[1]);
                case Instruction.IF: {
                    List<byte> conditionBytes = new List<byte>();
                    for (int i = 1; i < childInstructions.Count; i++) {
                        conditionBytes.AddRange(childInstructions[i]);
                    }
                    return Make_If(conditionBytes, childInstructions[0]);
                }
                case Instruction.UNLESS: {
                    List<byte> conditionBytes = new List<byte>();
                    for (int i = 1; i < childInstructions.Count; i++) {
                        conditionBytes.AddRange(childInstructions[i]);
                    }
                    return Make_Unless(conditionBytes, childInstructions[0]);
                }
                case Instruction.LIST_LENGTH:
                    return Make_ListLength(childInstructions[0]);
                case Instruction.CARD_HAS_TAG:
                    return Make_CardHasTag(childInstructions[0], childInstructions[1]);
                case Instruction.PLAYER_IS_WINNING:
                    return Make_PlayerIsWinning(childInstructions[0]);
                case Instruction.PLAYER_IS_LOSING:
                    return Make_PlayerIsLosing(childInstructions[0]);
                case Instruction.MULTIPLY:
                    return Make_Multiply(childInstructions[0], childInstructions[1]);
                case Instruction.LOOP: {
                    List<byte> loopBytes = new List<byte>();
                    for (int i = 1; i < childInstructions.Count; i++) {
                        loopBytes.AddRange(childInstructions[i]);
                    }
                    return Make_Loop(childInstructions[0], loopBytes);
                }
                case Instruction.FOR_LOOP: {
                    List<byte> loopBytes = new List<byte>();
                    for (int i = 1; i < childInstructions.Count; i++) {
                        loopBytes.AddRange(childInstructions[i]);
                    }
                    return Make_ForLoop(childInstructions[0], loopBytes);
                }
                case Instruction.ADD_TO_REGISTER: {
                    List<byte> bytesForRegister = new List<byte>();
                    for (int i = 1; i < childInstructions.Count; i++) {
                        bytesForRegister.AddRange(childInstructions[i]);
                    }
                    return Make_Unless(childInstructions[0], bytesForRegister);
                }
                // QUERIES
                case Instruction.GET_ACTIVE_PLAYER:
                    return Make_SingleByteInstruction(Instruction.GET_ACTIVE_PLAYER);
                case Instruction.GET_ALL_OPPONENTS: 
                    return Make_SingleByteInstruction(Instruction.GET_ALL_OPPONENTS);
                case Instruction.GET_ALL_PLAYERS: 
                    return Make_SingleByteInstruction(Instruction.GET_ALL_PLAYERS);
                case Instruction.GET_CARDS_IN_DECK:
                    return Make_SingleByteInstruction(Instruction.GET_CARDS_IN_DECK);
                case Instruction.GET_CARDS_IN_DISCARD: 
                    return Make_SingleByteInstruction(Instruction.GET_CARDS_IN_DISCARD);
                case Instruction.GET_CARDS_IN_HAND: 
                    return Make_GetCardsInHand(childInstructions[0]);
                case Instruction.GET_PLAYER:
                    return Make_GetPlayer(childInstructions[0]);
                case Instruction.GET_PLAYER_POINTS:
                    return Make_GetPlayerPoints(childInstructions[0]);
                case Instruction.IS_TRUE:
                    return Make_BoolComparison(
                        childInstructions[0],
                        LiteralFactory.CreateBoolLiteral(true),
                        (byte) ConditionOperator.EQUAL);
                case Instruction.IS_FALSE:
                    return Make_BoolComparison(
                        childInstructions[0],
                        LiteralFactory.CreateBoolLiteral(false),
                        (byte) ConditionOperator.EQUAL);
                case Instruction.READ_COUNTER:
                    return Make_ReadCounter(childInstructions[0]);
                case Instruction.NUM_COMPARISON:
                    return Make_NumComparison(childInstructions[0], childInstructions[1], childInstructions[2][0]);
                case Instruction.TARGET_PLAYER:
                    return Make_SingleByteInstruction(Instruction.TARGET_PLAYER);
                case Instruction.TARGET_CARD:
                    return Make_SingleByteInstruction(Instruction.TARGET_CARD);
                // EFFECTS
                case Instruction.INCREMENT_PLAYER_POINTS:
                    return Make_IncrementPlayerPoints(childInstructions[0], childInstructions[1]);
                case Instruction.PLAYER_DRAW_CARD:
                    return Make_PlayerDrawCards(childInstructions[0], childInstructions[1]);
                case Instruction.SET_COUNTER:
                    return Make_SetCounter(childInstructions[0], childInstructions[1]);
                case Instruction.SET_PLAYER_DRAW:
                    return Make_SetPlayerDraw(childInstructions[0], childInstructions[1]);
                case Instruction.SET_PLAYER_MAX_HAND:
                    return Make_SetPlayerMaxHand(childInstructions[0], childInstructions[1]);
                case Instruction.SET_PLAYER_POINTS:
                    return Make_SetPlayerPoints(childInstructions[0], childInstructions[1]);
                case Instruction.MOVE_TO_DECK:
                    return Make_MoveToDeck(childInstructions[0], childInstructions[1]);
                case Instruction.MOVE_TO_DISCARD:
                    return Make_MoveToDiscard(childInstructions[0]);
                // ENUMS
                case Instruction.ENUM_CONDITION_OPERATOR:
                    return LiteralFactory.CreateEnumLiteral(childInstructions[0][0], Instruction.ENUM_CONDITION_OPERATOR);
                case Instruction.ENUM_CONDITION_TYPE:
                    return LiteralFactory.CreateEnumLiteral(childInstructions[0][0], Instruction.ENUM_CONDITION_TYPE);
                case Instruction.ENUM_DECK_POSITION:
                    return LiteralFactory.CreateEnumLiteral(childInstructions[0][0], Instruction.ENUM_DECK_POSITION);
                case Instruction.ENUM_LIST_TYPE:
                    return LiteralFactory.CreateEnumLiteral(childInstructions[0][0], Instruction.ENUM_LIST_TYPE);
                default:
                    Debug.LogError($"Unsupported instruction {node.effectData.instruction}");
                    break;
            }
            
        } catch (ArgumentOutOfRangeException e) {
            Debug.LogError($"Wrong number of params for instruction {node.effectData.instruction}");
            Debug.LogError(e);
        }
        return new List<byte>();
    }

}