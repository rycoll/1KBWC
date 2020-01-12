using System.Collections.Generic;
using System;

/* These methods help ensure that each instruction type is built consistently! */

public class InstructionFactory {

    private static Random rng = new Random();
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

    public static List<byte> Make_SetPlayerDraw (List<byte> num, List<byte> player) {
        List<byte> bytes = new List<byte>(new List<byte>(num));
        bytes.AddRange(new List<byte>(player));
        bytes.Add((byte) Instruction.SET_PLAYER_DRAW);
        return bytes;
    }

    public static List<byte> Make_SetPlayerMaxHand (List<byte> num, List<byte> player) {
        List<byte> bytes = new List<byte>(new List<byte>(num));
        bytes.AddRange(new List<byte>(player));
        bytes.Add((byte) Instruction.SET_PLAYER_MAX_HAND);
        return bytes;
    }

    public static List<byte> Make_SetPlayerPoints (List<byte> num, List<byte> player) {
        List<byte> bytes = new List<byte>(new List<byte>(num));
        bytes.AddRange(new List<byte>(player));
        bytes.Add((byte) Instruction.SET_PLAYER_POINTS);
        return bytes;
    }

    public static List<byte> Make_IncrementPlayerPoints(List<byte> num, List<byte> player) {
        List<byte> bytes = new List<byte>(new List<byte>(num));
        bytes.AddRange(new List<byte>(player));
        bytes.Add((byte) Instruction.INCREMENT_PLAYER_POINTS);
        return bytes;
    }

    public static List<byte> Make_MoveToDeck(List<byte> card, DeckLocation location) {
        List<byte> bytes = new List<byte>(
            new List<byte>(
                LiteralFactory.CreateEnumLiteral((byte) location, Instruction.ENUM_DECK_POSITION)
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

    #endregion
}