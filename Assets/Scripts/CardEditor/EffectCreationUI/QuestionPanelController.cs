using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public struct QuestionnaireQueueItem {
    public EffectData parent;
    public FieldData data;

    public QuestionnaireQueueItem(EffectData effect, FieldData field) {
        parent = effect;
        data = field;
    }
}

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

    private List<QuestionnaireQueueItem> processingQueue = new List<QuestionnaireQueueItem>();
    private FieldData current;

    public void SetEffectBuilder(EffectBuilder b) {
        builder = b;
    }

    public void InitialState () {
        questionText.text = 
            "What would you like this card to do?\n\nChoose an effect from the list below to continue.";
        Dropdown(true, ReturnType.NONE);
        Input(false);

        List<string> options = EffectData.GetAllRootEffects()
            .Select(effect => effect.name).ToList();
        dropdown.ClearOptions();
        dropdown.AddOptions(options);
    }

    public void SetState (QuestionnaireQueueItem data) {
        current = data.data;
        questionText.text = $"{data.parent.message}\n\n{current.text}";
        Dropdown(current.returnType != ReturnType.NONE, current.returnType);
        Input(current.enterValue != EnterValueType.NONE, current.enterValue);
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
        dropdown.ClearOptions();
        dropdown.AddOptions(options);
    }

    public void Input (bool active, EnterValueType type = EnterValueType.NONE) {
        textInput.gameObject.SetActive(active);
        submitInputButton.gameObject.SetActive(active);

        switch(type) {
            case EnterValueType.NUMBER:
                textInput.contentType = InputField.ContentType.IntegerNumber;
                break;
            case EnterValueType.TEXT:
                textInput.contentType = InputField.ContentType.Alphanumeric;
                break;
        }
    }

    public void SubmitSelection () {
        string selection = dropdown.options[dropdown.value].text;

        // handling for enums
        if (current != null) {
            string typeString = current.returnType.ToString();
            if (typeString.StartsWith("ENUM")) {
                EnumRepesentation enumRepesentation = EnumRepesentation.EnumLookup(typeString);
                builder.Add((byte) enumRepesentation.getInstruction());
                builder.Add((byte) enumRepesentation.getIndex(selection));
                Next();
                return;
            }
        } 

        EffectData data = EffectData.GetEffectDataByName(selection);
        builder.Add((byte) data.instruction);

        List<QuestionnaireQueueItem> fields = data.fields.Select(
            item => new QuestionnaireQueueItem(data, item)
        ).ToList();
        processingQueue.InsertRange(0, fields);
        Next();
    }

    public void SubmitTextInput () {
        string selection = InputFieldText.text;

        switch(current.enterValue) {
            case EnterValueType.NUMBER: {
                if (Int32.TryParse(selection, out int numValue)) {
                    byte[] arr = LiteralFactory.CreateIntLiteral(numValue);
                    builder.Add(arr); 
                } else Debug.Log($"Couldn't parse {selection}");
                break;
            }
            case EnterValueType.TEXT: {
                byte[] arr = LiteralFactory.CreateStringLiteral(selection);
                builder.Add(arr);
                break;
            }
        }
        Next();
    }

    public void Next () {
        if (processingQueue.Count > 0) {
            QuestionnaireQueueItem next = processingQueue[0];
            processingQueue.Remove(next);
            SetState(next);
        } else {
            Last();
        }
    }

    public void Last () {

    }

}