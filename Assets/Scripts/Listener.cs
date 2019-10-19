using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Listener {

    private Condition Trigger { get; set; }
    private List<byte> Effects { get; set; }
    private bool DestroyOnTrigger { get; set; }

    public Listener (Condition trigger, List<byte> effectList, bool b) {
        Trigger = trigger;
        Effects = effectList;
        DestroyOnTrigger = b;
    }

    public void Check (GameMaster GM) {
        if (Trigger.Evaluate()) {
            GM.ExecuteEffects(Effects);
        }
    }
}