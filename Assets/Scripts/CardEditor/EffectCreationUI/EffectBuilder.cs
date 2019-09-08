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
        effectBytes.Add(b);
        PrintBytes();
    }

    public void Add (byte[] arr) {
        effectBytes.AddRange(arr);
        PrintBytes();
    }

    public byte[] ExportEffect () {
        byte[] arr = effectBytes.ToArray();
        effectBytes = new List<byte>();
        return arr;
    }

}