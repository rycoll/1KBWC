using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVariables {
    private List<string> flags;
    private Dictionary<string, string> variables;
    private Dictionary<string, int> counters;

    public GameVariables () {
        flags = new List<string>();
        variables = new Dictionary<string, string>();
        counters = new Dictionary<string, int>();
    }

    public void SetFlag(string flagName, bool add) {
        if (add) {
            if (!flags.Contains(flagName)) {
                flags.Add(flagName);
            }
        } else {
            flags.Remove(flagName);
        }
    }

    public bool IsFlag(string flagName) {
        return flags.Contains(flagName);
    }

    public void SetVariable(string key, string value) {
        variables[key] = value;
    }

    public string GetVariable(string key) {
        if (variables.ContainsKey(key)) {
            return variables[key];
        }
        return "";
    }

    public void SetCounter(string key, int value) {
        counters[key] = value;
    }

    public int GetCounter(string key) {
        if (counters.ContainsKey(key)) {
            return counters[key];
        }
        return 0;
    }
}