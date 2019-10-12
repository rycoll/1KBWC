using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RulesTextInterpreter : ByteManager
{
    private RulesTextCallback readAccessorFirst;
    private ReadCallback doNothing;
    public string ReadAccessorFirst () {
        return GetNext();
    }
    public void DoNothing () {}

    public RulesTextInterpreter(byte[] arr) {
        push(arr);
        readAccessorFirst = ReadAccessorFirst;
        doNothing = DoNothing;
    }

    new public string ReadEnumLiteral() {
        Instruction instruction = (Instruction) pop();
        byte value = pop();
        string typeString = instruction.ToString();
        if (typeString.StartsWith("ENUM")) {
            EnumRepesentation enumRepesentation = EnumRepesentation.EnumLookup(typeString);
            return enumRepesentation.getName((int) value);
        } else throw new UnexpectedByteException("Expected enum, got " + typeString);
    }

    public string ReadIntLiteral(RulesTextCallback cb) {
        if (NextInstructionIsAccessor()) {
            return cb();
        }
        try {
            CheckType(Instruction.INT);
            byte[] intRepresentation = pop(4);
            int num = BitConverter.ToInt32(intRepresentation, 0);
            return num.ToString();
        } catch (UnexpectedByteException e) {
            throw e;
        }
    }

    public string ReadStringLiteral(RulesTextCallback cb) {
        if (NextInstructionIsAccessor()) {
            return cb();
        }
        try {
            CheckType(Instruction.STRING);
            int arrSize = ReadIntLiteral(doNothing);
            byte[] strArray = pop(arrSize);
            char[] chars = System.Text.Encoding.UTF8.GetChars(strArray);
            return new string(chars);
        } catch (UnexpectedByteException e) {
            throw e;
        }
    }

    public string ReadPlayerLiteral(RulesTextCallback cb) {
        if (NextInstructionIsAccessor()) {
            return cb();
        }
        try {
            CheckType(Instruction.PLAYER);
            return $"Player {ReadIntLiteral(doNothing)}";
        } catch (UnexpectedByteException e) {
            throw e;
        }
    }

    public string ReadCardLiteral(RulesTextCallback cb) {
        if (NextInstructionIsAccessor()) {
            return cb();
        }
        try {
            CheckType(Instruction.CARD);
            return  $"Card {ReadIntLiteral(doNothing)}";
        } catch (UnexpectedByteException e) {
            throw e;
        }
    }

    public string ReadBoolLiteral(RulesTextCallback cb) {
        if (NextInstructionIsAccessor()) {
            return cb();
        }
        try {
            CheckType(Instruction.BOOL);
            return (pop() != 0) ? "true" : "false";
        } catch (UnexpectedByteException e) {
            throw e;
        }
    }

    public string ReadConditionLiteral(RulesTextCallback cb) {
        if (NextInstructionIsAccessor()) {
            return cb();
        } else throw new UnexpectedByteException("Expected a condition-getting instruction, got " + (Instruction) pop());
    }

    public string ReadList (RulesTextCallback cb) {
        if (NextInstructionIsAccessor()) {
            return cb();
        } else throw new UnexpectedByteException("Expected a list-getting instruction, got " + (Instruction) pop());
        
    }

    public string GetFullRulesText () {
        string text = "";

        while (currentStackSize > 0) {
            text += GetNext();
        }

        return text;
    }

    public string GetNext() {
        Instruction instruction = this.next();
        EffectData effectData = EffectData.Effects.First(effect => effect.instruction == instruction);
        string[] args = new string[effectData.fields.Length];
        switch(instruction) {
            // [card]
            case Instruction.MOVE_TO_DISCARD:
                args[0] = ReadCardLiteral(readAccessorFirst);
                break;
            // [int]
            case Instruction.RANDOM_NUMBER:
            case Instruction.GET_PLAYER:
                args[0] = ReadIntLiteral(readAccessorFirst);
                break;
            // [int, int]
            case Instruction.ADD:
            case Instruction.MULTIPLY:
                args[0] = ReadIntLiteral(readAccessorFirst);
                args[1] = ReadIntLiteral(readAccessorFirst);
                break;
            // [int, player]
            case Instruction.SET_PLAYER_POINTS:
            case Instruction.INCREMENT_PLAYER_POINTS:
            case Instruction.SET_PLAYER_DRAW:
            case Instruction.SET_PLAYER_MAX_HAND:
            case Instruction.PLAYER_DRAW_CARD:
                args[0] = ReadPlayerLiteral(readAccessorFirst);
                args[1] = ReadIntLiteral(readAccessorFirst);
                break;
            // [list]
            case Instruction.LIST_LENGTH:
                args[0] = ReadList(readAccessorFirst);
                break;
            // [player]
            case Instruction.GET_CARDS_IN_HAND:
            case Instruction.GET_PLAYER_POINTS:
                args[0] = ReadPlayerLiteral(readAccessorFirst);
                break;
            // [string]
            case Instruction.READ_COUNTER:
                args[0] = ReadStringLiteral(readAccessorFirst);
                break;
            // others
            case Instruction.BOOL_COMPARISON:
                args[0] = ReadBoolLiteral(readAccessorFirst);
                args[1] = ReadBoolLiteral(readAccessorFirst);
                args[2] = ReadEnumLiteral();
                break;
            case Instruction.FOR_LOOP:
                args[0] = ReadList(readAccessorFirst);
                args[1] = GetNext();
                break;
            case Instruction.IF:
                args[0] = ReadConditionLiteral(readAccessorFirst);
                args[1] = GetNext();
                break;
            case Instruction.LOOP:
                args[0] = ReadIntLiteral(readAccessorFirst);
                args[1] = GetNext();
                break;
            case Instruction.MOVE_TO_DECK:
                args[0] = ReadCardLiteral(readAccessorFirst);
                args[1] = ReadEnumLiteral();
                break;
            case Instruction.NUM_COMPARISON:
                args[0] = ReadIntLiteral(readAccessorFirst);
                args[1] = ReadIntLiteral(readAccessorFirst);
                args[2] = ReadEnumLiteral();
                break;
            case Instruction.SET_COUNTER:
                args[0] = ReadStringLiteral(readAccessorFirst);
                args[1] = ReadIntLiteral(readAccessorFirst);
                break;
            // no args
            default:
                break;
        }

        return RulesTextBuilder.GetInstructionText(effectData, args);
    }
}

public delegate string RulesTextCallback();