using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardField<T> {

    public RunTimeValue<T> Value { get; set; }
    public int lookup { get; set; }
    public bool useLookup { get; set; }

    

}