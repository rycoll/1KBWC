using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* These methods help ensure that each instruction type is built consistently! */

public class InstructionFactory {
    private static int forloopID = 0;
    public static int ForLoopID {
        get {
            return forloopID++;
        }
    }

    public static byte[] Make_GetPlayer (byte[] id) {
        // id, head
        List<byte> bytes = new List<byte>();
        bytes.AddRange(new List<byte>(id));
        bytes.Add((byte) Instruction.GET_PLAYER);
        return bytes.ToArray();
    }

    public static byte[] Make_GetPlayerPoints (byte[] id) {
        // id, head
        List<byte> bytes = new List<byte>();
        bytes.AddRange(new List<byte>(id));
        bytes.Add((byte) Instruction.GET_PLAYER_POINTS);
        return bytes.ToArray();
    }

    public static byte[] Make_Loop (byte[] num, byte[] code) {
        // endloop, code, num, head
        List<byte> bytes = new List<byte>();
        bytes.Add((byte) Instruction.ENDLOOP);
        bytes.AddRange(new List<byte>(code));
        bytes.AddRange(new List<byte>(num));
        bytes.Add((byte) Instruction.LOOP);
        return bytes.ToArray();
    }

    public static byte[] Make_ForLoop (byte[] itemList, byte[] code) {
        // endloop, code, list, id, head
        List<byte> bytes = new List<byte>();
        bytes.Add((byte) Instruction.ENDLOOP);
        bytes.AddRange(new List<byte>(code));
        bytes.AddRange(new List<byte>(itemList));
        bytes.AddRange(new List<byte>(
            Interpreter.CreateIntLiteral(ForLoopID)
        ));
        bytes.Add((byte) Instruction.FOR_LOOP);
        return bytes.ToArray();
    }

    public static byte[] Make_PlayerDrawCards (byte[] player, byte[] num) {
        List<byte> bytes = new List<byte>();
        bytes.Add((byte) Instruction.ENDLOOP);
        bytes.AddRange(new List<byte>(num));
        bytes.AddRange(new List<byte>(player));
        bytes.Add((byte) Instruction.LOOP);
        return bytes.ToArray();
    }
}