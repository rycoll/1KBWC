using System;
using System.Collections.Generic;
using UnityEngine;

public class PrintStack : ByteManager {

    private ReadCallback readAccessorFirst;

    public PrintStack(byte[] bytes, int size) {
        stack = new byte[MAX_STACK_SIZE];
        for (int i = 0; i < size; i++) {
            push(bytes[i]);
        }
        readAccessorFirst = ReadAccessorFirst;
    }

    public static void PrintByteString (byte[] bytes) {
        Debug.Log(new PrintStack(bytes, bytes.Length).ReportStackContent());
    }

    public static void PrintRawBytes (byte[] bytes) {
        string str = "";
        foreach (byte b in bytes) { str += b.ToString() + " "; }
        Debug.Log(str);
    }

    public void ReadAccessorFirst () {
        PrintNext();
    }
    
    public string PrintStackInstructions () {
        byte[] temp = stack;
        string print = "";
        while (HasBytes()){
            print += $"{PrintNext()} ";
        }
        stack = temp;
        return print;
    }

    public string PrintNext () { 
        Instruction instruction = (Instruction) peek();
        try {
            if (Array.IndexOf(enumInstructions, instruction) != -1) {
                byte b = ReadEnumLiteral();
                return $"enum:{b}";
            }
            switch (instruction) {
                case Instruction.INT: {
                    int n = ReadIntLiteral(readAccessorFirst);
                    return $"{instruction.ToString()}({n})";
                }
                case Instruction.STRING: {
                    string s = ReadStringLiteral(readAccessorFirst);
                    return $"{instruction.ToString()}({s})";
                }
                case Instruction.BOOL: {
                    bool b = ReadBoolLiteral(readAccessorFirst);
                    return $"{instruction.ToString()}({b})";
                }
                case Instruction.PLAYER: {
                    int player = ReadPlayerLiteral(readAccessorFirst);
                    return $"{instruction.ToString()}({player})";
                }
                case Instruction.CARD: {
                    int card = ReadCardLiteral(readAccessorFirst);
                    return $"{instruction.ToString()}({card})";
                }
                case Instruction.LIST: {
                    // pop head and ENUM_LIST_TYPE head
                    pop();
                    byte type = ReadEnumLiteral();
                    int size = ReadIntLiteral(readAccessorFirst);

                    push(LiteralFactory.CreateIntLiteral(size));
                    push(LiteralFactory.CreateEnumLiteral(type, Instruction.ENUM_LIST_TYPE));
                    push((byte) Instruction.LIST);
                    ReadList(readAccessorFirst);

                    string typeName = EnumRepesentation.EnumLookup("ENUM_LIST_TYPE").getName((int) type);
                    return $"{instruction.ToString()}(size:{size},type:{typeName})";
                }
                case Instruction.IF:
                case Instruction.UNLESS:
                case Instruction.LOOP:
                case Instruction.FOR_LOOP:
                case Instruction.ENDIF:
                case Instruction.ENDLOOP: {
                    pop();
                    int id = ReadIntLiteral(readAccessorFirst);
                    return $"{instruction.ToString()}(id:{id})";
                }
                case Instruction.PLACEHOLDER: {
                    pop();
                    int id = ReadIntLiteral(readAccessorFirst);
                    return $"{instruction.ToString()}(id:{id})";
                }
                default: {
                    pop();
                    return instruction.ToString();
                }
            }
        } catch (UnexpectedByteException e) {
            Debug.LogError($"Printer jam! ({instruction}) --- {e}");
            return "#" + ((byte) instruction).ToString();
        } catch (StackFullException e) {
            Debug.LogError($"Printer jam! ({instruction}) --- {e}");
            return "#" + ((byte) instruction).ToString();
        } catch (StackEmptyException e) {
            Debug.LogError($"Printer jam! ({instruction}) --- {e}");
            return "#" + ((byte) instruction).ToString();
        }
    }

}