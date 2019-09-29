using System;
using System.Collections.Generic;

public class ByteManager {

    public static int MAX_STACK_SIZE = 2058;
    protected int currentStackSize = 0;
    protected byte[] stack = new byte[MAX_STACK_SIZE];


    public int GetCurrentStackSize () {
        return currentStackSize;
    }

    public bool HasBytes () {
        return currentStackSize > 0;
    }

    public void ClearStack () {
        stack = new byte[MAX_STACK_SIZE];
        currentStackSize = 0;
    }

    public string ReportStackContent () {
        PrintStack printer = new PrintStack(stack, currentStackSize);
        return printer.PrintStackInstructions();
    }

    #region Basic Stack Operations

    public void push(byte b) {
        if (currentStackSize >= MAX_STACK_SIZE) {
            throw new StackFullException("Stack is too full! Can't push " + b.ToString());
        }
        stack[currentStackSize++] = b;
    }

    public void push(byte[] arr) {
        if (currentStackSize + arr.Length >= MAX_STACK_SIZE) {
            throw new StackFullException("Stack is too full! Can't push " + arr.Length + " bytes!");
        }
        foreach (byte b in arr) {
            push(b);
        }
    }

    public byte pop() {
        // needs better error handling
        if (!HasBytes()) {
            throw new StackEmptyException("Pop failed, stack is empty! " + currentStackSize);
        }
        stack[currentStackSize] = 0;
        return stack[--currentStackSize];
    }

    public byte[] pop(int n) {
        byte[] arr = new byte[n];
        for (int i = 0; i < n; i++) {
            arr[i] = pop();
        }
        return arr;
    }

    public byte peek() {
        if (!HasBytes()) {
            throw new StackEmptyException("Peek failed, stack is empty! " + currentStackSize);
        }
        return stack[currentStackSize - 1];
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
        return top < 0060 && top >= 0030;
    }

    #endregion

    #region Read Literals

    public byte ReadEnumLiteral() {
        try {
            CheckType(new Instruction[]{
                Instruction.ENUM_CONDITION_OPERATOR,
                Instruction.ENUM_DECK_POSITION
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
            byte[] intRepresentation = pop(4);
            return BitConverter.ToInt32(intRepresentation, 0);
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
            byte[] strArray = pop(arrSize);
            char[] chars = System.Text.Encoding.UTF8.GetChars(strArray);
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

    public int ReadCardLiteral(ReadCallback cb) {
        if (NextInstructionIsAccessor()) {
            cb();
        }
        try {
            CheckType(Instruction.CARD);
            return ReadIntLiteral(cb);
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
                    bool b = ReadBoolLiteral(cb);
                    ConditionOperator op = (ConditionOperator) ReadEnumLiteral();
                    return new Condition(a, b, op);
                }
                case ConditionType.NUM: {
                    int a = ReadIntLiteral(cb);
                    int b = ReadIntLiteral(cb);
                    ConditionOperator op = (ConditionOperator) ReadEnumLiteral();
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
            // discard list type
            pop();
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
            CheckType(ListType.PLAYER);
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
    public List<int> ReadCardList (ReadCallback cb) {
        if (NextInstructionIsAccessor()) {
            cb();
        }
        try {
            CheckType(Instruction.LIST);
            CheckType(ListType.CARD);
            int num = ReadIntLiteral(cb);
            List<int> cards = new List<int>();
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
        if (currentStackSize <= 0) {
            throw new StackEmptyException($"Stack is empty: ({currentStackSize}), can't do next!");
        }
        return (Instruction) pop();
    }

}