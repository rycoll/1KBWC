using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionFactory {
    private static int forloopID = 0;
    public static int ForLoopID {
        get {
            return forloopID++;
        }
    } 

    public static byte[] Make_GetPlayer (int id) {
        List<byte> bytes = new List<byte>();
        bytes.Add((byte) Instruction.GET_PLAYER);
        bytes.AddRange(new List<byte>(
            Interpreter.CreateIntLiteral(id)
        ));
        return bytes.ToArray();
    }

    public static byte[] Make_GetPlayerPoints (int id) {
        List<byte> bytes = new List<byte>();
        bytes.Add((byte) Instruction.GET_PLAYER_POINTS);
        bytes.AddRange(new List<byte>(
            Interpreter.CreateIntLiteral(id)
        ));
        return bytes.ToArray();
    }

    public static byte[] Make_Loop (int num, byte[] code) {
        List<byte> bytes = new List<byte>();
        bytes.Add((byte) Instruction.LOOP);
        bytes.AddRange(new List<byte>(
            Interpreter.CreateIntLiteral(num)
        ));
        bytes.AddRange(new List<byte>(code));
        bytes.Add((byte) Instruction.ENDLOOP);
        return bytes.ToArray();
    }

    public static byte[] Make_ForLoop (byte[] itemList, byte[] code) {
        List<byte> bytes = new List<byte>();
        bytes.Add((byte) Instruction.FOR_LOOP);
        bytes.AddRange(new List<byte>(
            Interpreter.CreateIntLiteral(ForLoopID)
        ));
        bytes.AddRange(new List<byte>(itemList));
        bytes.AddRange(new List<byte>(code));
        bytes.Add((byte) Instruction.ENDLOOP);
        return bytes.ToArray();
    }
}