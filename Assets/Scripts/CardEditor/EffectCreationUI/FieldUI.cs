using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldUI : MonoBehaviour
{
    public Text headerText;
    public GameObject toggleButton;
    public Text toggleButtonText;

    private FieldData data;

    public GameObject effectPrefab;

    public Toggle boolEnterField;
    public InputField stringEnterField;
    public InputField numberEnterField;
    public Dropdown selectField;

    private List<GameObject> children;

    private void Awake () {
        children = new List<GameObject>();
    }

    public void SetData (FieldData data) {
        this.data = data;
        OpenDisplay();
    }

    public void OpenDisplay () {
        headerText.text = data.text;
        
        selectField.ClearOptions();
        List<string> options = new List<string>();
        foreach (EffectData effectData in data.GetDropdownValues()) {
            options.Add(effectData.name);
        }
        selectField.AddOptions(options);
        selectField.onValueChanged.AddListener(delegate {
            SetSubQuery(GetDataForCurrentSelected());
        });
        selectField.gameObject.SetActive(false);

        if (data.enterValue == EnterValueType.NONE) {
            toggleButton.SetActive(false);
            SelectQueryMode();
        } else {
            EnterValueMode();
        }        
    }

    public void Toggle () {
        if (data.enterValue == EnterValueType.NONE || !selectField.gameObject.activeSelf) {
            SelectQueryMode();
        } else {
            EnterValueMode();
        }
    }

    public GameObject ChooseAppropriateEnterField () {
        switch (data.enterValue) {
            case EnterValueType.BOOL:
                return boolEnterField.gameObject;
            case EnterValueType.NUMBER:
                return numberEnterField.gameObject;
            case EnterValueType.TEXT:
                return stringEnterField.gameObject;
            default:
                return null;
        }  
    }

    public void DeactivateAllFields () {
        boolEnterField.gameObject.SetActive(false);
        stringEnterField.gameObject.SetActive(false);
        numberEnterField.gameObject.SetActive(false);
        selectField.gameObject.SetActive(false);

        // should maybe clear all of these too
        ClearSubQuery();
    }

    public void EnterValueMode () {
        DeactivateAllFields();
        toggleButtonText.text = "Enter a value";
        GameObject enterField = ChooseAppropriateEnterField();
        enterField.SetActive(false);
    }

    public void SelectQueryMode () {
        DeactivateAllFields();
        toggleButtonText.text = "Read a value";
        selectField.gameObject.SetActive(true);
        SetSubQuery(GetDataForCurrentSelected());
    }

    public EffectData GetDataForCurrentSelected () {
        if (!selectField.gameObject.activeSelf || selectField.options.Count == 0) {
            return null;
        }
        string selection = selectField.options[selectField.value].text;
        return EffectData.GetEffectDataByName(selection);
    }

    public void ClearSubQuery () {
        foreach (GameObject child in children) {
            Destroy(child);
        }
        children = new List<GameObject>();
    }

    public void SetSubQuery (EffectData effect) {
        ClearSubQuery();
        if (effect != null) {
            GameObject effectObj = Instantiate(effectPrefab, this.transform) as GameObject;
            //effectObj.GetComponent<EffectUI>().SetEffect(effect);
        }
    }

    public GameObject AddPrefabToDisplay (GameObject obj) {
        return Instantiate(obj, this.transform) as GameObject;
    }

}
