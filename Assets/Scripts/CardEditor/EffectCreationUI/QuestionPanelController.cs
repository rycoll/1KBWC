using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class QuestionPanelController : MonoBehaviour
{
    public EffectTabController controller;

    public Text questionText;
    public Dropdown dropdown;
    public InputField textInput;
    public Text InputFieldText;
    public Button submitSelectionButton;
    public Button submitInputButton;

    private EffectBuilder builder;

    private EffectBuilderItem currentCompilerNode;
    private FieldData currentFieldData;

    public void Clear () {
        builder = null;
        currentCompilerNode = null;
        currentFieldData = null;
    }

    public void InitialState () {
        Clear();

        questionText.text  = 
            "What would you like this card to do?\n\nChoose an effect from the list below to continue.";
        Dropdown(true, ReturnType.NONE);
        Input(false);

        List<string> options = EffectData.GetAllRootEffects()
            .Select(effect => effect.name).ToList();
        dropdown.ClearOptions();
        dropdown.AddOptions(options);
    }

    public void SetState () {
        questionText.text = $"{currentFieldData.text}";
        if (currentCompilerNode.parent != null) {
            questionText.text.Insert(0, $"{currentCompilerNode.parent.effectData.message}\n\n");
        }

        Dropdown(currentFieldData.returnType != ReturnType.NONE, currentFieldData.returnType);
        Input(currentFieldData.enterValue != EnterValueType.NONE, currentFieldData.enterValue);
    }

    public void Dropdown (bool active, ReturnType type = ReturnType.NONE) {
        dropdown.gameObject.SetActive(active);
        submitSelectionButton.gameObject.SetActive(active);

        // set appropriate dropdown options with ReturnType
        List<string> options;

        // handling for enums
        string typeString = type.ToString();
        if (typeString.StartsWith("ENUM")) {
            EnumRepesentation enumRepesentation = EnumRepesentation.EnumLookup(typeString);
            options = enumRepesentation.getNames().ToList();
        } else if (type == ReturnType.ROOT_EFFECT) {
            options = EffectData.GetAllRootEffects()
                .Select(effect => effect.name).ToList();
        } else {
            options = EffectData.Effects
                .Where(effect => effect.returnType == type && effect.type != "primitive")
                .Select(effect => effect.name).ToList();
        }

        // need to check all ancestors for potential placeholders

        dropdown.ClearOptions();
        dropdown.AddOptions(options);
    }

    public void Input (bool active, EnterValueType type = EnterValueType.NONE) {
        textInput.gameObject.SetActive(active);
        textInput.text = "";
        submitInputButton.gameObject.SetActive(active);

        switch(type) {
            case EnterValueType.NUMBER:
                textInput.contentType = InputField.ContentType.IntegerNumber;
                break;
            case EnterValueType.TEXT:
                textInput.contentType = InputField.ContentType.Alphanumeric;
                break;
            case EnterValueType.NONE:
                break;
            default:
                Debug.Log($"Unhandled enter-value type: {type}");
                break;
        }
    }

    public void SubmitSelection () {
        string selection = dropdown.options[dropdown.value].text;

        EffectData data = EffectData.GetEffectDataByName(selection);
        EffectBuilderItem newNode = data != null
            ? new EffectBuilderItem(data)
            : null;

        // handling for enums
        if (currentCompilerNode != null) {
            string typeString = currentFieldData.returnType.ToString();
            if (typeString.StartsWith("ENUM")) {
                EnumRepesentation enumRepesentation = EnumRepesentation.EnumLookup(typeString);
                Instruction enumInstruction = enumRepesentation.getInstruction();
                EffectData effectDataForEnumInstruction = EffectData.GetEffectDataByInstruction(enumInstruction);
                EffectBuilderItem compilerNode = new EffectBuilderItem(effectDataForEnumInstruction);
                currentCompilerNode.Add(compilerNode);
                byte enumValue = (byte) enumRepesentation.getIndex(selection);
                compilerNode.Add(new EffectBuilderItem(new List<byte>{enumValue}));
                Next();
                return;
            } else if (newNode != null) {
                currentCompilerNode.Add(newNode);
            }
        }

        if (newNode != null) {
            currentCompilerNode = newNode;
        }
        Next();
    }

    public void SubmitTextInput () {
        string selection = InputFieldText.text;

        if (String.IsNullOrEmpty(selection)) {
            return;
        }

        switch(currentFieldData.enterValue) {
            case EnterValueType.NUMBER: {
                if (Int32.TryParse(selection, out int numValue)) {
                    List<byte> arr = LiteralFactory.CreateIntLiteral(numValue);
                    currentCompilerNode.Add(
                        new EffectBuilderItem(arr)
                    ); 
                } else Debug.Log($"Couldn't parse {selection}");
                break;
            }
            case EnterValueType.TEXT: {
                List<byte> arr = LiteralFactory.CreateStringLiteral(selection);
                currentCompilerNode.Add(
                    new EffectBuilderItem(arr)
                );
                break;
            }
            default:
                Debug.LogWarning("Unsupported text input type: " + currentFieldData.enterValue);
                break;
        }
        Next();
    }

    public void Next () {

        if (builder == null) {
            builder = new EffectBuilder(currentCompilerNode);
        }

        FieldData nextFieldData = currentCompilerNode.GetNextFieldData();

        if (nextFieldData == null) {
            if (currentCompilerNode.parent == null) {
                Last();
                return;
            } else {
                currentCompilerNode = currentCompilerNode.parent;
                Next();
                return;
            }
        }

        currentFieldData = nextFieldData;

        // handling for auxiliary bytes
        if (Array.IndexOf(currentFieldData.attributes, "auxiliary") != -1) {
            string typeString = currentFieldData.returnType.ToString();
            Instruction instruction = EffectData.GetEffectDataByName(typeString).instruction;
            currentCompilerNode.Add(
                new EffectBuilderItem(new List<byte>{(byte) instruction})
            );
            Next();
            return;
        }

        SetState();
    }

    public void Last () {
        builder.PrintBytes();
        List<byte> exportCurrent = builder.ExportEffect();

        controller.AddEffect(exportCurrent.ToArray());
        controller.OpenSummaryPanel();
    }

}