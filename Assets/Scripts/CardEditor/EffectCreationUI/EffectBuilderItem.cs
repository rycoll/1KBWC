using System.Collections.Generic;

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

    public FieldData GetNextFieldData () {
        if (processingQueue.Count == 0) {
            return null;
        }
        FieldData nextFieldData = processingQueue[0];
        processingQueue.Remove(nextFieldData);
        return nextFieldData;
    }

}