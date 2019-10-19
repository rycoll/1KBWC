using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameListeners {
    private List<Listener> listeners;

    public void Start () {
        listeners = new List<Listener>();
    }

    public void AddListener (Listener l) {
        listeners.Add(l);
    }

    public void RunListeners (GameMaster GM) {
        foreach (Listener listener in listeners) {
            listener.Check(GM);
        }
    }
}