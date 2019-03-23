using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class RegularEffect : CardEffect {
    public abstract void Run(GameController gameController);

}