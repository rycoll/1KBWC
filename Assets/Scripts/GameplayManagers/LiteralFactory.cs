using System;
using System.Collections.Generic;

public class LiteralFactory {

    public static byte[] CreateIntLiteral(int n) {
        // getbytes uses same endian format as the system
        byte[] intRepresentation = BitConverter.GetBytes(n);
        Array.Reverse(intRepresentation);
        List<byte> bytes = new List<byte>();
        bytes.AddRange(new List<byte>(intRepresentation));
        bytes.Add((byte) Instruction.INT);
        return bytes.ToArray();
    }

    public static byte[] CreateStringLiteral(string str) {
        byte[] arr = System.Text.Encoding.UTF8.GetBytes(str);
        Array.Reverse(arr);
        byte[] strSize = CreateIntLiteral(arr.Length);
        List<byte> bytes = new List<byte>();
        bytes.AddRange(new List<byte>(arr));
        bytes.AddRange(new List<byte>(strSize));
        bytes.Add((byte) Instruction.STRING);
        return bytes.ToArray();
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
        List<byte> bytes = new List<byte>();
        bytes.AddRange(new List<byte>(CreateIntLiteral(n)));
        bytes.Add((byte) Instruction.CARD);
        return bytes.ToArray();
    }

    public static byte[] CreateBoolLiteral(bool b) {
        byte[] boolArr = new byte[2];
        boolArr[0] = b ? (byte) 1 : (byte) 0;
        boolArr[1] = (byte) Instruction.BOOL;
        return boolArr;
    }

    public static byte[] CreateConditionLiteral(byte[] operandA, byte[] operandB, ConditionType t, ConditionOperator op) {
        // use this to create a reusable condition that can run queries
        List<byte> bytes = new List<byte>();
        bytes.AddRange(new List<byte>(CreateEnumLiteral((byte) op, Instruction.ENUM_CONDITION_OPERATOR)));
        bytes.AddRange(new List<byte>(operandB));
        bytes.AddRange(new List<byte>(operandA));
        bytes.AddRange(new List<byte>(CreateEnumLiteral((byte) t, Instruction.ENUM_CONDITION_OPERATOR)));
        bytes.Add((byte) Instruction.CONDITION);
        return bytes.ToArray();
    }

    public static byte[] CreateConditionLiteral(CompareBool condition) {
        byte[] operandA = CreateBoolLiteral(condition.Operand);
        byte[] operandB = CreateBoolLiteral(condition.CheckValue);

        List<byte> bytes = new List<byte>();
        bytes.AddRange(new List<byte>(CreateEnumLiteral((byte) condition.Operator, Instruction.ENUM_CONDITION_OPERATOR)));
        bytes.AddRange(new List<byte>(operandB));
        bytes.AddRange(new List<byte>(operandA));
        bytes.AddRange(new List<byte>(CreateEnumLiteral((byte) ConditionType.BOOL, Instruction.ENUM_CONDITION_OPERATOR)));
        bytes.Add((byte) Instruction.CONDITION);
        return bytes.ToArray();
    }

    public static byte[] CreateConditionLiteral(CompareNum condition) {
        byte[] operandA = CreateIntLiteral(condition.OperandA);
        byte[] operandB = CreateIntLiteral(condition.OperandB);

        List<byte> bytes = new List<byte>();
        bytes.AddRange(new List<byte>(CreateEnumLiteral((byte) condition.Operator, Instruction.ENUM_CONDITION_OPERATOR)));
        bytes.AddRange(new List<byte>(operandB));
        bytes.AddRange(new List<byte>(operandA));
        bytes.AddRange(new List<byte>(CreateEnumLiteral((byte) ConditionType.NUM, Instruction.ENUM_CONDITION_OPERATOR)));
        bytes.Add((byte) Instruction.CONDITION);
        return bytes.ToArray();
    }

    public static byte[] CreateListLiteral(byte[] objects, ListType type, int length) {
        byte[] listSize = CreateIntLiteral(length);

        List<byte> bytes = new List<byte>();
        bytes.AddRange(new List<byte>(objects));
        bytes.AddRange(new List<byte>(listSize));
        bytes.Add((byte) type);
        bytes.Add((byte) Instruction.LIST);
        return bytes.ToArray();
    }
    public static byte[] CreateListLiteral(List<GamePlayer> players) {
        List<byte> bytes = new List<byte>();
        foreach (GamePlayer player in players) {
            List<byte> playerBytes = new List<byte>(CreatePlayerLiteral(player));
            bytes.AddRange(playerBytes);
            bytes.AddRange(new List<byte>(CreateIntLiteral(playerBytes.Count)));
            bytes.Add((byte) Instruction.LIST_ITEM);
        }
        return CreateListLiteral(bytes.ToArray(), ListType.PLAYER, players.Count);
    }
    public static byte[] CreateListLiteral(List<Card> cards) {
        List<byte> bytes = new List<byte>();
        foreach (Card card in cards) {
            List<byte> cardBytes = new List<byte>(CreateCardLiteral(card.GetID()));
            bytes.AddRange(cardBytes);
            bytes.AddRange(new List<byte>(CreateIntLiteral(cardBytes.Count)));
            bytes.Add((byte) Instruction.LIST_ITEM);
        }
        return CreateListLiteral(bytes.ToArray(), ListType.CARD, cards.Count);
    }

    public static byte[] CreatePlaceholderLiteral(int id) {
        List<byte> bytes = new List<byte>();
        bytes.AddRange(CreateIntLiteral(id));
        bytes.Add((byte) Instruction.PLACEHOLDER);
        return bytes.ToArray();
    }

    public static byte[] CreateChunkLiteral(byte[] srcBytes) {
        List<byte> bytes = new List<byte>();
        bytes.AddRange(srcBytes);
        bytes.AddRange(CreateIntLiteral(srcBytes.Length));
        bytes.Add((byte) Instruction.CHUNK);
        return bytes.ToArray();
    }

    public static byte[] CreateEnumLiteral(byte b, Instruction enumType) {
        return new byte[]{b, (byte) enumType};
    }

}