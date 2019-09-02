using System.Collections;
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
        questionText.text = data.text;
        Dropdown(data.returnType != ReturnType.NONE, data.returnType);
        Input(data.enterValue != EnterValueType.NONE, data.enterValue);
    }

    public void Dropdown (bool active, ReturnType type = ReturnType.NONE) {
        dropdown.gameObject.SetActive(active);
        submitSelectionButton.gameObject.SetActive(active);
    }

    public void Input (bool active, EnterValueType type = EnterValueType.NONE) {
        textInput.gameObject.SetActive(active);
        submitInputButton.gameObject.SetActive(active);
    }

    public void SubmitSelection () {
        string selection = dropdown.options[dropdown.value].text;
    }

    public void SubmitTextInput () {
        string selection = InputFieldText.text;
    }


}