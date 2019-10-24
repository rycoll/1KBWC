using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class ParsedEffectList {
    public List<ParsedEffect> effects;
}

[System.Serializable]
public class ParsedEffect {
    public string name;
    public string type;
    public string instruction;
    public string message;
    public string returnType;
    public ParsedField[] fields;
    public bool canBeRoot;
}

[System.Serializable]
public class ParsedField {
    public string type;
    public string desc;
    public string[] attributes;
}

public class EffectJSONParser {

    public static List<EffectData> GetEffectDataFromJSONFile() {
        return ConvertParsedJSON(
            ParseJSON(
                ReadJSON("Assets/effects.json")
            )
        );
    }

    public static List<EffectData> ConvertParsedJSON (List<ParsedEffect> parsedList) {
        List<EffectData> effects = new List<EffectData>();
        foreach (ParsedEffect parsed in parsedList) {
            FieldData[] fieldArr = new FieldData[parsed.fields.Length];
            for (int i = 0; i < parsed.fields.Length; i++) {
                fieldArr[i] = ConvertParsedField(parsed.fields[i]);
            }
            
            bool success = Enum.TryParse(parsed.instruction, out Instruction instruction);
            if (!success) throw new UnexpectedEnumException("Bad instruction type: " + parsed.instruction);

            success = Enum.TryParse(parsed.returnType, out ReturnType returnType);
            if (!success) throw new UnexpectedEnumException("Bad return type: " + parsed.returnType);

            effects.Add(new EffectData(
                instruction, parsed.name, parsed.type, parsed.message, fieldArr, parsed.canBeRoot, returnType 
            ));
        }
        return effects;
    }

    public static FieldData ConvertParsedField (ParsedField parsedField) {
        bool success = Enum.TryParse(parsedField.type, out ReturnType type);
        if (!success) throw new UnexpectedEnumException("Bad return type: " + parsedField.type);
        EnterValueType enterType = EnterValueType.NONE;
        switch(type) {
            case ReturnType.BOOL:
                enterType = EnterValueType.BOOL;
                break;
            case ReturnType.NUMBER:
                enterType = EnterValueType.NUMBER;
                break;
            case ReturnType.TEXT:
                enterType = EnterValueType.TEXT;
                break;
        }
        return new FieldData(){
            text = parsedField.desc,
            enterValue = enterType,
            returnType = type,
            attributes = parsedField.attributes
        };
    }

    public static List<ParsedEffect> ParseJSON (string json) {
        return JsonUtility.FromJson<ParsedEffectList>(json).effects;
    }

    public static string ReadJSON (string path) {
        using (StreamReader reader = new StreamReader(path)) {
            string json = reader.ReadToEnd();
            return json;
        }
    }

}