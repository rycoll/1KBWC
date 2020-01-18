using System.Collections.Generic;
using UnityEngine;

public class EffectBuilder {

    private EffectBuilderItem rootNode;

    public EffectBuilder(EffectBuilderItem root) {
        rootNode = root;
    }

    public string PrintNode (EffectBuilderItem node) {
        string print = "";
        if (node.effectData != null) {
            print += node.effectData.instruction.ToString() + " ";
        }
        if (node.enteredValue != null && node.enteredValue.Count > 0) {
            print += "( ";
            for (int i = 0; i < node.enteredValue.Count; i++) {
                print += node.enteredValue[i] + " ";
            }
            print += ") ";
        }
        if (node.children != null  && node.children.Count > 0) {
            print += "[ ";
            foreach (EffectBuilderItem child in node.children) {
                print += PrintNode(child);
            }
            print += "] ";
        }
        return print;
    }

    public void PrintBytes () {
        Debug.Log(PrintNode(rootNode));
    }

    public List<byte> ExportEffect () {
        return InstructionFactory.RunInstructionFactoryForNode(rootNode);
    }
    
}

public class EffectBuilderItem {
    public EffectData effectData;
    public EffectBuilderItem parent;
    public List<EffectBuilderItem> children = new List<EffectBuilderItem>();

    public List<FieldData> processingQueue = new List<FieldData>();

    public List<byte> enteredValue = new List<byte>();

    public EffectBuilderItem(EffectData effect) {
        effectData = effect;
        if (effect.fields != null) {
            processingQueue = new List<FieldData>(effect.fields);
        }
        children = new List<EffectBuilderItem>();
    }

    public EffectBuilderItem(List<byte> value) {
        enteredValue = value;
    }

    public void Add(EffectBuilderItem child) {
        children.Add(child);
        child.parent = this;   
    }

}