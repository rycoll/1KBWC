using System.Collections.Generic;
using UnityEngine;

public class EffectBuilder {

    private List<byte> effectBytes;

    public EffectBuilder() {
        effectBytes = new List<byte>();
    }

    public void PrintBytes () {
        string print = "bytes: ";
        foreach (byte b in effectBytes) {
            print += b.ToString() + " ";
        }
        Debug.Log(print);
    }

    public void Add (byte b) {
        effectBytes.Insert(0, b);
        PrintBytes();
    }

    public void Add (byte[] arr) {
        effectBytes.InsertRange(0, arr);
        PrintBytes();
    }

    public byte[] ExportEffect (bool print) {
        byte[] arr = effectBytes.ToArray();
        effectBytes = new List<byte>();

        if (print) {
            PrintStack printer = new PrintStack(arr, arr.Length);
            Debug.Log(printer.PrintStackInstructions());
        }

        return arr;
    }

}