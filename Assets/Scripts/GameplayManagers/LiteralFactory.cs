using System;
using System.Collections.Generic;

public class LiteralFactory {

    public static List<byte> CreateIntLiteral(int n) {
        // getbytes uses same endian format as the system
        List<byte> intRepresentation = new List<byte>(BitConverter.GetBytes(n));
        intRepresentation.Reverse();
        List<byte> bytes = new List<byte>();
        bytes.AddRange(new List<byte>(intRepresentation));
        bytes.Add((byte) Instruction.INT);
        return bytes;
    }

    public static List<byte> CreateStringLiteral(string str) {
        List<byte> arr = new List<byte>(System.Text.Encoding.UTF8.GetBytes(str));
        arr.Reverse();
        List<byte> strSize = CreateIntLiteral(arr.Count);
        List<byte> bytes = new List<byte>();
        bytes.AddRange(new List<byte>(arr));
        bytes.AddRange(new List<byte>(strSize));
        bytes.Add((byte) Instruction.STRING);
        return bytes;
    }

    public static List<byte> CreatePlayerLiteral(GamePlayer player) {
        return CreatePlayerLiteral(player.Index);
    }
    public static List<byte> CreatePlayerLiteral(int n) {
        List<byte> bytes = new List<byte>();
        bytes.AddRange(new List<byte>(CreateIntLiteral(n)));
        bytes.Add((byte) Instruction.PLAYER);
        return bytes;
    }

    public static List<byte> CreateCardLiteral(string id) {
        List<byte> bytes = new List<byte>();
        bytes.AddRange(new List<byte>(CreateStringLiteral(id)));
        bytes.Add((byte) Instruction.CARD);
        return bytes;
    }

    public static List<byte> CreateBoolLiteral(bool b) {
        List<byte> boolArr = new List<byte>();
        boolArr.Add(b ? (byte) 1 : (byte) 0);
        boolArr.Add((byte) Instruction.BOOL);
        return boolArr;
    }

    public static List<byte> CreateConditionLiteral(List<byte> operandA, List<byte> operandB, ConditionType t, ConditionOperator op) {
        // use this to create a reusable condition that can run queries
        List<byte> bytes = new List<byte>();
        bytes.AddRange(new List<byte>(operandB));
        bytes.AddRange(new List<byte>(CreateEnumLiteral((byte) op, Instruction.ENUM_CONDITION_OPERATOR)));
        bytes.AddRange(new List<byte>(operandA));
        bytes.AddRange(new List<byte>(CreateEnumLiteral((byte) t, Instruction.ENUM_CONDITION_TYPE)));
        bytes.Add((byte) Instruction.CONDITION);
        return bytes;
    }

    public static List<byte> CreateConditionLiteral(CompareBool condition) {
        List<byte> operandA = CreateBoolLiteral(condition.Operand);
        List<byte> operandB = CreateBoolLiteral(condition.CheckValue);

        List<byte> bytes = new List<byte>();
        bytes.AddRange(new List<byte>(operandB));
        bytes.AddRange(new List<byte>(CreateEnumLiteral((byte) condition.Operator, Instruction.ENUM_CONDITION_OPERATOR)));
        bytes.AddRange(new List<byte>(operandA));
        bytes.AddRange(new List<byte>(CreateEnumLiteral((byte) ConditionType.BOOL, Instruction.ENUM_CONDITION_TYPE)));
        bytes.Add((byte) Instruction.CONDITION);
        return bytes;
    }

    public static List<byte> CreateConditionLiteral(CompareNum condition) {
        List<byte> operandA = CreateIntLiteral(condition.OperandA);
        List<byte> operandB = CreateIntLiteral(condition.OperandB);

        List<byte> bytes = new List<byte>();
        bytes.AddRange(new List<byte>(operandB));
        bytes.AddRange(new List<byte>(CreateEnumLiteral((byte) condition.Operator, Instruction.ENUM_CONDITION_OPERATOR)));
        bytes.AddRange(new List<byte>(operandA));
        bytes.AddRange(new List<byte>(CreateEnumLiteral((byte) ConditionType.NUM, Instruction.ENUM_CONDITION_TYPE)));
        bytes.Add((byte) Instruction.CONDITION);
        return bytes;
    }

    public static List<byte> CreateListLiteral(List<byte> objects, ListType type, int length) {
        List<byte> listSize = CreateIntLiteral(length);
        List<byte> listType = CreateEnumLiteral((byte) type, Instruction.ENUM_LIST_TYPE);

        List<byte> bytes = new List<byte>();
        bytes.AddRange(new List<byte>(objects));
        bytes.AddRange(new List<byte>(listSize));
        bytes.AddRange(listType);
        bytes.Add((byte) Instruction.LIST);
        return bytes;
    }
    public static List<byte> CreateListLiteral(List<GamePlayer> players) {
        List<byte> bytes = new List<byte>();
        foreach (GamePlayer player in players) {
            List<byte> playerBytes = new List<byte>(CreatePlayerLiteral(player));
            bytes.AddRange(playerBytes);
            bytes.AddRange(new List<byte>(CreateIntLiteral(playerBytes.Count)));
            bytes.Add((byte) Instruction.LIST_ITEM);
        }
        return CreateListLiteral(bytes, ListType.PLAYER, players.Count);
    }
    public static List<byte> CreateListLiteral(List<Card> cards) {
        List<byte> bytes = new List<byte>();
        foreach (Card card in cards) {
            List<byte> cardBytes = new List<byte>(CreateCardLiteral(card.GetID()));
            bytes.AddRange(cardBytes);
            bytes.AddRange(new List<byte>(CreateIntLiteral(cardBytes.Count)));
            bytes.Add((byte) Instruction.LIST_ITEM);
        }
        return CreateListLiteral(bytes, ListType.CARD, cards.Count);
    }

    public static List<byte> CreatePlaceholderLiteral(int id) {
        List<byte> bytes = new List<byte>();
        bytes.AddRange(CreateIntLiteral(id));
        bytes.Add((byte) Instruction.PLACEHOLDER);
        return bytes;
    }

    public static List<byte> CreateEnumLiteral(byte b, Instruction enumType) {
        return new List<byte>{b, (byte) enumType};
    }

}