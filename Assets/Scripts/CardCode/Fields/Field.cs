using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Field<T> {

    public RunTimeValue<T> Value { get; set; }
    public int lookup { get; set; }
    public bool useLookup { get; set; }

    public abstract T Evaluate (GameController gameController);

}
