using System.Collections.Generic;
using UnityEngine;

public class EffectBuilder {

    private List<byte> effectBytes;

    public EffectBuilder() {
        effectBytes = new List<byte>();
    }

    public void Add (byte b) {
        effectBytes.Add(b);
        Debug.Log(effectBytes);
    }

    public void Add (byte[] arr) {
        effectBytes.AddRange(arr);
        Debug.Log(effectBytes);
    }

    public byte[] ExportEffect () {
        return effectBytes.ToArray();
    }

}