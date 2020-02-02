using System;
using System.Collections.Generic;
using UnityEngine;

public class ByteManager {

    protected List<byte> stack = new List<byte>();

    protected Dictionary<int, List<byte>> register = new Dictionary<int, List<byte>>();

    private const byte accessorRangeLowBound = 0030;
    private const byte accessorRangeHighBound = 0100;
    protected Instruction[] enumInstructions = {
        Instruction.ENUM_CONDITION_OPERATOR,
        Instruction.ENUM_DECK_POSITION,
        Instruction.ENUM_CONDITION_TYPE
    };

    public int GetCurrentStackSize () {
        return stack.Count;
    }

    public bool HasBytes () {
        return stack.Count > 0;
    }

    public void ClearStack () { 
        stack = new List<byte>();
    }

    public string ReportStackContent () {
        PrintStack printer = new PrintStack(stack);
        return printer.PrintStackInstructions();
    }
    public void PrintRawBytes () {
        PrintStack.PrintRawBytes(stack);
    }

    #region Basic Stack Operations

    public void push(byte b) {
        stack.Add(b);
    }

    public void push(List<byte> arr) {
        foreach (byte b in arr) {
            push(b);
        }
    }

    public byte pop() {
        // needs better error handling
        if (!HasBytes()) {
            throw new StackEmptyException("Pop failed, stack is empty!");
        }
        byte topByte = stack[stack.Count - 1];
        stack.RemoveAt(stack.Count - 1);
        return topByte;
    }

    public List<byte> pop(int n) {
        List<byte> arr = new List<byte>();
        for (int i = 0; i < n; i++) {
            arr.Add(pop());
        }
        return arr;
    }

    public List<byte> popInstruction(ReadCallback cb) {
        // bytes returned in reverse order, so they can be pushed straight back on
        List<byte> bytes = new List<byte>();
        byte b = peek();
        switch ((Instruction) b) {
            case Instruction.INT:
                for (int i = 0; i < 5; i++) {
                    bytes.Insert(0, pop());
                }
                break;
            case Instruction.STRING: 
                byte headInstruction = pop();
                int size = ReadIntLiteral(cb);
                List<byte> sizeBytes = LiteralFactory.CreateIntLiteral(size);
                List<byte> charBytes = pop(size);

                bytes.Insert(0, headInstruction);
                for (int i = sizeBytes.Count - 1; i >= 0; i--) {
                    bytes.Insert(0, sizeBytes[i]);
                }
                for (int i = 0; i < charBytes.Count; i++) {
                    bytes.Insert(0, charBytes[i]);
                }
                break;
            case Instruction.BOOL:
            case Instruction.ENUM_CONDITION_OPERATOR:
            case Instruction.ENUM_CONDITION_TYPE:
            case Instruction.ENUM_DECK_POSITION:
            case Instruction.ENUM_LIST_TYPE:
                bytes.Insert(0, pop());
                bytes.Insert(0, pop());
                break;
            default:
                bytes.Add(pop());
                break;
        }
        return bytes;
    }

    public byte peek() {
        if (!HasBytes()) {
            throw new StackEmptyException("Peek failed, stack is empty!");
        }
        return stack[stack.Count - 1];
    }

    public byte peek(int n) {
        if (!HasBytes()) {
            throw new StackEmptyException("Peek failed, stack is empty!");
        }
        try {
            return stack[stack.Count - n];
        } catch (IndexOutOfRangeException e) {
            Debug.Log("Couldn't peek " + n + " bytes");
            Debug.Log(ReportStackContent());
            throw e;
        }
    }

    #endregion

    #region Type Checking

    public bool CheckType (Instruction check) {
        byte top = peek();
        if (top == (byte) check) {
            pop();
            return true;
        } else throw new UnexpectedByteException("Expected " + check + ", Found " + top);
    }
    public bool CheckType (Instruction[] checks) {
        byte top = peek();
        string expected = "";
        for (int i = 0; i < checks.Length; i++) {
            if (top == (byte) checks[i]) {
                pop();
                return true;
            }
            expected += $" {checks[i].ToString()},";
        }
        throw new UnexpectedByteException($"Expected {expected} Found {top}");
    }
    public bool CheckType (ListType check) {
        byte top = peek();
        if (top == (byte) check) {
            pop();
            return true;
        } else throw new UnexpectedByteException("Expected " + check + ", Found " + top);
    }

    public bool NextInstructionIsAccessor () {
        byte top = peek();
        return top < accessorRangeHighBound && top >= accessorRangeLowBound;
    }

    #endregion

    #region Read Literals

    public byte ReadEnumLiteral() {
        try {
            CheckType(new Instruction[]{
                Instruction.ENUM_CONDITION_OPERATOR,
                Instruction.ENUM_DECK_POSITION,
                Instruction.ENUM_CONDITION_TYPE,
                Instruction.ENUM_LIST_TYPE
            });
            return pop();
        } catch (UnexpectedByteException e) {
            throw e;
        }
    }

    public int ReadIntLiteral(ReadCallback cb) {
        if (NextInstructionIsAccessor()) {
            cb();  
        }
        try {
            CheckType(Instruction.INT);
            List<byte> intRepresentation = pop(4);
            return BitConverter.ToInt32(intRepresentation.ToArray(), 0);
        } catch (UnexpectedByteException e) {
            throw e;
        }
    }

    public string ReadStringLiteral(ReadCallback cb) {
        if (NextInstructionIsAccessor()) {
            cb();
        }
        try {
            CheckType(Instruction.STRING);
            int arrSize = ReadIntLiteral(cb);
            List<byte> strArray = pop(arrSize);
            char[] chars = System.Text.Encoding.UTF8.GetChars(strArray.ToArray());
            return new string(chars);
        } catch (UnexpectedByteException e) {
            throw e;
        }
    }

    public int ReadPlayerLiteral(ReadCallback cb) {
        if (NextInstructionIsAccessor()) {
            cb();
        }
        try {
            CheckType(Instruction.PLAYER);
            return ReadIntLiteral(cb);
        } catch (UnexpectedByteException e) {
            throw e;
        }
    }

    public string ReadCardLiteral(ReadCallback cb) {
        if (NextInstructionIsAccessor()) {
            cb();
        }
        try {
            CheckType(Instruction.CARD);
            return ReadStringLiteral(cb);
        } catch (UnexpectedByteException e) {
            throw e;
        }
    }

    public bool ReadBoolLiteral(ReadCallback cb) {
        if (NextInstructionIsAccessor()) {
            cb();
        }
        try {
            CheckType(Instruction.BOOL);
            return pop() != 0;
        } catch (UnexpectedByteException e) {
            throw e;
        }
    }

    public Condition ReadConditionLiteral(ReadCallback cb) {
        if (NextInstructionIsAccessor()) {
            cb();
        }
        try {
            CheckType(Instruction.CONDITION);
            byte conditionType = ReadEnumLiteral();
            switch ((ConditionType) conditionType) {
                case ConditionType.BOOL: {
                    bool a = ReadBoolLiteral(cb);
                    ConditionOperator op = (ConditionOperator) ReadEnumLiteral();
                    bool b = ReadBoolLiteral(cb);
                    return new Condition(a, b, op);
                }
                case ConditionType.NUM: {
                    int a = ReadIntLiteral(cb);
                    ConditionOperator op = (ConditionOperator) ReadEnumLiteral();
                    int b = ReadIntLiteral(cb);
                    return new Condition(a, b, op);
                }
                default:
                    throw new UnexpectedByteException("Didn't understand the condition type! " + conditionType);
            }
        } catch (UnexpectedByteException e) {
            throw e;
        }
    }

    public List<byte[]> ReadList (ReadCallback cb) {
        if (NextInstructionIsAccessor()) {
            cb();
        }
        try {
            CheckType(Instruction.LIST);
            // discard list type (enum head + enum byte)
            pop(2);
            int num = ReadIntLiteral(cb);
            List<byte[]> items = new List<byte[]>();
            for (int i = 0; i < num; i++) {
                CheckType(Instruction.LIST_ITEM);
                int size = ReadIntLiteral(cb);
                byte[] array = new byte[size];
                for (int j = 0; j < size; j++) {
                    array[j] = pop();
                }
                items.Add(array);
            }
            return items;
        } catch (UnexpectedByteException e) {
            throw e;
        }
    }

    public List<int> ReadPlayerList (ReadCallback cb) {
        if (NextInstructionIsAccessor()) {
            cb();
        }
        try {
            CheckType(Instruction.LIST);
            ListType listType = (ListType) ReadEnumLiteral();
            if (listType != ListType.PLAYER) {
                throw new UnexpectedByteException("Expected player list, got list of type " + listType);
            }
            int num = ReadIntLiteral(cb);
            List<int> players = new List<int>();
            for (int i = 0; i < num; i++) {
                CheckType(Instruction.LIST_ITEM);
                // discard size
                ReadIntLiteral(cb);
                players.Add(ReadPlayerLiteral(cb));
            }
            return players;
        } catch (UnexpectedByteException e) {
            throw e;
        }
    }
    public List<string> ReadCardList (ReadCallback cb) {
        if (NextInstructionIsAccessor()) {
            cb();
        }
        try {
            CheckType(Instruction.LIST);
            ListType listType = (ListType) ReadEnumLiteral();
            if (listType != ListType.CARD) {
                throw new UnexpectedByteException("Expected card list, got list of type " + listType);
            }
            int num = ReadIntLiteral(cb);
            List<string> cards = new List<string>();
            for (int i = 0; i < num; i++) {
                CheckType(Instruction.LIST_ITEM);
                // discard size
                ReadIntLiteral(cb);
                cards.Add(ReadCardLiteral(cb));
            }
            return cards;
        } catch (UnexpectedByteException e) {
            throw e;
        }
    }

    #endregion

    public Instruction next () {
        if (!HasBytes()) {
            throw new StackEmptyException($"Stack is empty, can't do next!");
        }
        return (Instruction) pop();
    }

}