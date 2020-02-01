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

