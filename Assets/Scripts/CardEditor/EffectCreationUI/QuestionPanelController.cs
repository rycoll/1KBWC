using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class QuestionPanelController : MonoBehaviour
{
    public Text questionText;
    public Dropdown dropdown;
    public InputField textInput;
    public Text InputFieldText;
    public Button submitSelectionButton;
    public Button submitInputButton;

    private EffectBuilder builder;

    private List<FieldData> processingQueue = new List<FieldData>();
    private FieldData current;

    public void SetEffectBuilder(EffectBuilder b) {
        builder = b;
    }

    public void InitialState () {
        questionText.text = 
            "What would you like this card to do?\n\nChoose an effect from the list below to continue.";
        Dropdown(true, ReturnType.NONE);
        Input(false);

        List<string> options = EffectData.InstructionDataMap.Values
            .Where(value => EffectData.BaseInstructions.Contains(value.instruction))
            .Select(value => value.name).ToList();
        dropdown.ClearOptions();
        dropdown.AddOptions(options);
    }

    public void SetState (FieldData data) {
        current = data;
        questionText.text = data.text;
        Dropdown(data.returnType != ReturnType.NONE, data.returnType);
        Input(data.enterValue != EnterValueType.NONE, data.enterValue);
    }

    public void Dropdown (bool active, ReturnType type = ReturnType.NONE) {
        Debug.Log($"Dropdown active: {active} - {type}");
        dropdown.gameObject.SetActive(active);
        submitSelectionButton.gameObject.SetActive(active);

        // set appropriate dropdown options with ReturnType
        List<string> options = EffectData.InstructionDataMap.Values
            .Where(value => type == value.returnType)
            .Select(value => value.name).ToList();
        dropdown.ClearOptions();
        dropdown.AddOptions(options);
    }

    public void Input (bool active, EnterValueType type = EnterValueType.NONE) {
        Debug.Log($"Input active: {active} - {type}");

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

        EffectData data = EffectData.GetEffectDataByName(selection);
        builder.Add((byte) data.instruction);

        processingQueue.InsertRange(0, data.fields);
        Next();
    }

    public void SubmitTextInput () {
        string selection = InputFieldText.text;
        // number: parse int
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
            FieldData next = processingQueue[0];
            processingQueue.Remove(next);
            SetState(next);
        } else {
            Last();
        }
    }

    public void Last () {

    }

}