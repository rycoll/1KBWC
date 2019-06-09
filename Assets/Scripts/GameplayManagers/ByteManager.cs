using System;
using System.Collections.Generic;
using UnityEngine;

public class ByteManager {

    private const int MAX_STACK_SIZE = 2058;
    private int currentStackSize = 0;
    private byte[] stack = new byte[MAX_STACK_SIZE];

    private GameMaster GM;

    public int GetCurrentStackSize () {
        return currentStackSize;
    }

    public bool hasBytes () {
        return currentStackSize > 0;
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
        if (currentStackSize <= 0) {
            throw new StackEmptyException("Stack is empty! " + currentStackSize);
        }
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
        if (currentStackSize <= 0) {
            throw new StackEmptyException("Stack is empty! " + currentStackSize);
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
    public bool CheckType (ListType check) {
        byte top = peek();
        if (top == (byte) check) {
            pop();
            return true;
        } else throw new UnexpectedByteException("Expected " + check + ", Found " + top);
    }
    public bool NextInstructionIsAccessor () {
        byte top = peek();
        return top < 0x60 && top >= 0x30;
    }

    #endregion

    #region Read Literals

    public int ReadIntLiteral() {
        if (NextInstructionIsAccessor()) {
            next();
        }
        try {
            CheckType(Instruction.INT);
            byte[] intRepresentation = pop(4);
            return BitConverter.ToInt32(intRepresentation, 0);
        } catch (UnexpectedByteException e) {
            throw e;
        }
    }

    public string ReadStringLiteral() {
        if (NextInstructionIsAccessor()) {
            next();
        }
        try {
            CheckType(Instruction.STRING);
            byte arrSize = pop();
            byte[] strArray = pop(arrSize);
            char[] chars = System.Text.Encoding.UTF8.GetChars(strArray);
            return new string(chars);
        } catch (UnexpectedByteException e) {
            throw e;
        }
    }

    public int ReadPlayerLiteral() {
        if (NextInstructionIsAccessor()) {
            next();
        }
        try {
            CheckType(Instruction.PLAYER);
            return ReadIntLiteral();
        } catch (UnexpectedByteException e) {
            throw e;
        }
    }

    public int ReadCardLiteral() {
        if (NextInstructionIsAccessor()) {
            next();
        }
        try {
            CheckType(Instruction.CARD);
            return ReadIntLiteral();
        } catch (UnexpectedByteException e) {
            throw e;
        }
    }

    public bool ReadBoolLiteral() {
        if (NextInstructionIsAccessor()) {
            next();
        }
        try {
            CheckType(Instruction.BOOL);
            return pop() != 0;
        } catch (UnexpectedByteException e) {
            throw e;
        }
    }

    public Condition ReadConditionLiteral() {
        if (NextInstructionIsAccessor()) {
            next();
        }
        try {
            CheckType(Instruction.CONDITION);
            byte conditionType = pop();
            switch ((ConditionType) conditionType) {
                case ConditionType.NUM:
                    int a = ReadIntLiteral();
                    int b = ReadIntLiteral();
                    ConditionOperator op = (ConditionOperator) pop();
                    return new Condition(a, b, op);
                default:
                    throw new UnexpectedByteException("Didn't understand the condition type! " + conditionType);
            }
        } catch (UnexpectedByteException e) {
            throw e;
        }
    }

    public List<byte[]> ReadList () {
        if (NextInstructionIsAccessor()) {
            next();
        }
        try {
            CheckType(Instruction.LIST);
            // discard list type
            pop();
            int num = ReadIntLiteral();
            List<byte[]> items = new List<byte[]>();
            for (int i = 0; i < num; i++) {
                CheckType(Instruction.LIST_ITEM);
                int size = ReadIntLiteral();
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

    public List<int> ReadPlayerList () {
        if (NextInstructionIsAccessor()) {
            next();
        }
        try {
            CheckType(Instruction.LIST);
            CheckType(ListType.PLAYER);
            int num = ReadIntLiteral();
            List<int> players = new List<int>();
            for (int i = 0; i < num; i++) {
                CheckType(Instruction.LIST_ITEM);
                // discard size
                ReadIntLiteral();
                players.Add(ReadPlayerLiteral());
            }
            return players;
        } catch (UnexpectedByteException e) {
            throw e;
        }
    }
    public List<int> ReadCardList () {
        if (NextInstructionIsAccessor()) {
            next();
        }
        try {
            CheckType(Instruction.LIST);
            CheckType(ListType.CARD);
            int num = ReadIntLiteral();
            List<int> cards = new List<int>();
            for (int i = 0; i < num; i++) {
                CheckType(Instruction.LIST_ITEM);
                // discard size
                ReadIntLiteral();
                cards.Add(ReadCardLiteral());
            }
            return cards;
        } catch (UnexpectedByteException e) {
            throw e;
        }
    }

    #endregion

    public Instruction next () {
        if (currentStackSize <= 0) {
            throw new StackEmptyException($"Stack is empty: ({currentStackSize}, can't do next!");
        }
        return (Instruction) pop();
    }

}