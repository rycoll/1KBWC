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
                    pop();
                    ListType type = (ListType) pop();
                    int size = ReadIntLiteral(readAccessorFirst);

                    push(LiteralFactory.CreateIntLiteral(size));
                    push((byte) type);
                    push((byte) Instruction.LIST);
                    ReadList(readAccessorFirst);

                    return $"{instruction.ToString()}(size:{size},type:{type})";
                }
                case Instruction.FOR_LOOP: {
                    pop();
                    int id = ReadIntLiteral(readAccessorFirst);
                    return $"{instruction.ToString()}(id:{id})";
                }
                case Instruction.PLACEHOLDER: {
                    pop();
                    int id = ReadIntLiteral(readAccessorFirst);
                    return $"{instruction.ToString()}(id:{id})";
                }
                case Instruction.CHUNK: {
                    pop();
                    int size = ReadIntLiteral(readAccessorFirst);
                    return $"{instruction.ToString()}(size:{size})";
                }
                default: {
                    pop();
                    return instruction.ToString();
                }
            }
        } catch (UnexpectedByteException e) {
            Debug.LogError($"Printer jam! ({instruction}) --- {e}");
            return ((byte) instruction).ToString();
        } catch (StackFullException e) {
            Debug.LogError($"Printer jam! ({instruction}) --- {e}");
            return ((byte) instruction).ToString();
        } catch (StackEmptyException e) {
            Debug.LogError($"Printer jam! ({instruction}) --- {e}");
            return ((byte) instruction).ToString();
        }
    }

}