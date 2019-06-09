using System;
using System.Collections.Generic;

public class LiteralFactory {

    public static byte[] CreateIntLiteral(int n) {
        // getbytes uses same endian format as the system
        byte[] intRepresentation = BitConverter.GetBytes(n);
        List<byte> bytes = new List<byte>();
        bytes.AddRange(new List<byte>(intRepresentation));
        bytes.Add((byte) Instruction.INT);
        return bytes.ToArray();
    }

    public static byte[] CreateStringLiteral(string str) {
        byte[] arr = System.Text.Encoding.UTF8.GetBytes(str);
        byte[] strBytes = new byte[255];
        if (arr.Length <= 255) {
            strBytes = arr;
        } else {   
            Array.Copy(arr, strBytes, 255);
        }
        byte[] literal = new byte[strBytes.Length + 2];
        literal[0] = (byte) Instruction.STRING;
        literal[1] = (byte) strBytes.Length;
        strBytes.CopyTo(literal, 2);
        return literal;
    }


    public static byte[] CreatePlayerLiteral(GamePlayer player) {
        return CreatePlayerLiteral(player.Index);
    }
    public static byte[] CreatePlayerLiteral(int n) {
        List<byte> bytes = new List<byte>();
        bytes.AddRange(new List<byte>(CreateIntLiteral(n)));
        bytes.Add((byte) Instruction.PLAYER);
        return bytes.ToArray();
    }

    public static byte[] CreateCardLiteral(int n) {
        byte[] intRepresentation = CreateIntLiteral(n);
        byte[] literal = new byte[intRepresentation.Length + 1];
        literal[0] = (byte) Instruction.PLAYER;
        intRepresentation.CopyTo(literal, 1);
        return literal;
    }

    public static byte[] CreateBoolLiteral(bool b) {
        byte[] boolArr = new byte[2];
        boolArr[0] = (byte) Instruction.BOOL;
        boolArr[1] = b ? (byte) 1 : (byte) 0;
        return boolArr;
    }

    public static byte[] CreateConditionLiteral(byte[] operandA, byte[] operandB, ConditionOperator op) {
        // use this to create a reusable condition that can run queries
        byte[] conditionArr = new byte[2 + operandA.Length + operandB.Length];
        conditionArr[0] = (byte) ConditionType.NUM;
        conditionArr[1] = (byte) op;
        operandA.CopyTo(conditionArr, 2);
        operandB.CopyTo(conditionArr, 2 + operandA.Length);
        return conditionArr;
    }

    public static byte[] CreateConditionLiteral(CompareNum condition) {
        byte[] operandA = CreateIntLiteral(condition.OperandA);
        byte[] operandB = CreateIntLiteral(condition.OperandB);
        byte[] conditionArr = new byte[2 + operandA.Length + operandB.Length];
        conditionArr[0] = (byte) ConditionType.NUM;
        conditionArr[1] = (byte) condition.Operator;
        operandA.CopyTo(conditionArr, 2);
        operandB.CopyTo(conditionArr, 2 + operandA.Length);
        return conditionArr;
    }

    public static byte[] CreateListLiteral(byte[] objects, ListType type, int length) {
        byte[] listSize = CreateIntLiteral(length);
        byte[] listLiteral = new byte[2 + listSize.Length + objects.Length];
        listLiteral[0] = (byte) Instruction.LIST;
        listLiteral[1] = (byte) type;
        objects.CopyTo(listLiteral, 2);
        return listLiteral;
    }
    public static byte[] CreateListLiteral(List<GamePlayer> players) {
        List<byte> bytes = new List<byte>();
        foreach (GamePlayer player in players) {
            bytes.AddRange(
                new List<byte>(CreatePlayerLiteral(player))
            );
        }
        return CreateListLiteral(bytes.ToArray(), ListType.PLAYER, players.Count);
    }
    public static byte[] CreateListLiteral(List<Card> cards) {
        List<byte> bytes = new List<byte>();
        foreach (Card card in cards) {
            bytes.AddRange(
                new List<byte>(CreateCardLiteral(card.id))
            );
        }
        return CreateListLiteral(bytes.ToArray(), ListType.CARD, cards.Count);
    }

}