using System.Collections.Generic;

/* These methods help ensure that each instruction type is built consistently! */

public class InstructionFactory {
    private static int forloopID = 0;
    public static int ForLoopID {
        get {
            return forloopID++;
        }
    }

    public static byte[] Make_Add(byte[] a, byte[] b) {
        List<byte> bytes = new List<byte>();
        bytes.AddRange(new List<byte>(a));
        bytes.AddRange(new List<byte>(b));
        bytes.Add((byte) Instruction.ADD);
        return bytes.ToArray();
    }

    public static byte[] Make_Multiply(byte[] a, byte[] b) {
        List<byte> bytes = new List<byte>();
        bytes.AddRange(new List<byte>(a));
        bytes.AddRange(new List<byte>(b));
        bytes.Add((byte) Instruction.MULTIPLY);
        return bytes.ToArray();
    }

    public static byte[] Make_ListLength(byte[] list) {
        List<byte> bytes = new List<byte>();
        bytes.AddRange(new List<byte>(list));
        bytes.Add((byte) Instruction.LIST_LENGTH);
        return bytes.ToArray();
    }

    public static byte[] Make_If(byte[] code, byte[] condition) {
        List<byte> bytes = new List<byte>();
        bytes.AddRange(new List<byte>(code));
        bytes.AddRange(new List<byte>(condition));
        bytes.Add((byte) Instruction.IF);
        return bytes.ToArray();
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

    public static byte[] Make_CodeWithPlaceholders (List<byte[]> chunks, int id) {
        List<byte> bytes = new List<byte>();
        for (int i = 0; i < chunks.Count; i++) {
            bytes.AddRange(LiteralFactory.CreateChunkLiteral(chunks[i]));
            if (i < chunks.Count - 1) {
                bytes.AddRange(LiteralFactory.CreatePlaceholderLiteral(id));
            }
        }
        return bytes.ToArray();
    } 

    public static byte[] Make_ForLoop (byte[] itemList, List<byte[]> codeChunks) {
        // endloop, code, list, id, head
        int id = ForLoopID;
        byte[] code = Make_CodeWithPlaceholders(codeChunks, id);

        List<byte> bytes = new List<byte>();
        bytes.Add((byte) Instruction.ENDLOOP);
        bytes.AddRange(new List<byte>(code));
        bytes.AddRange(new List<byte>(itemList));
        bytes.AddRange(new List<byte>(
            LiteralFactory.CreateIntLiteral(id)
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