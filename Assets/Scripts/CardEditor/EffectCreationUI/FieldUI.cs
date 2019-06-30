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

    public GameObject stringFieldPrefab;
    public GameObject numberFieldPrefab;
    public GameObject dropdownPrefab;
    public GameObject fieldPrefab;

    private GameObject currEnterField;
    private GameObject currSelectField;
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
        
        GameObject dropdownObj = AddPrefabToDisplay(this.dropdownPrefab);
        Dropdown dropdown = dropdownObj.GetComponent<Dropdown>();
        dropdown.ClearOptions();
        List<string> options = new List<string>();
        foreach (EffectData effectData in data.GetDropdownValues()) {
            options.Add(effectData.name);
        }
        dropdown.AddOptions(options);
        dropdown.onValueChanged.AddListener(delegate {
            SetSubQuery(GetDataForCurrentSelected());
        });
        dropdownObj.SetActive(false);
        currSelectField = dropdownObj;

        switch (data.enterValue) {
            case EnterValueType.BOOL:
                // this aint right
                currEnterField = AddPrefabToDisplay(this.stringFieldPrefab);
                EnterValueMode();
                break;
            case EnterValueType.NONE:
                toggleButton.SetActive(false);
                SelectQueryMode();
                break;
            case EnterValueType.NUMBER:
                currEnterField = AddPrefabToDisplay(this.numberFieldPrefab);
                EnterValueMode();
                break;
            case EnterValueType.TEXT:
                currEnterField = AddPrefabToDisplay(this.stringFieldPrefab);
                EnterValueMode();
                break;
        }        
    }

    public void Toggle () {
        if (data.enterValue == EnterValueType.NONE || currEnterField.activeSelf) {
            SelectQueryMode();
        } else {
            EnterValueMode();
        }
    }

    public void EnterValueMode () {
        ClearSubQuery();
        toggleButtonText.text = "Enter a value";
        if (currEnterField) {
            currEnterField.SetActive(true);
        }
        if (currSelectField) {
            currSelectField.SetActive(false);
        }
    }

    public void SelectQueryMode () {
        toggleButtonText.text = "Read a value";
        if (currEnterField) {
            currEnterField.SetActive(false);
        }
        if (currSelectField) {
            currSelectField.SetActive(true);
        }
        SetSubQuery(GetDataForCurrentSelected());
    }

    public EffectData GetDataForCurrentSelected () {
        if (!currSelectField) {
            // this sucks
            return null;
        }
        Dropdown dropdown = currSelectField.GetComponent<Dropdown>();
        if (dropdown.options.Count == 0) {
            // this also sucks
            return null;
        }
        string selection = dropdown.options[dropdown.value].text;
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
            foreach (FieldData fieldData in effect.fields) {
                GameObject newChild = Instantiate(fieldPrefab, this.transform) as GameObject;
                newChild.GetComponent<FieldUI>().SetData(fieldData);
                children.Add(newChild);
            }
        }
    }

    public GameObject AddPrefabToDisplay (GameObject obj) {
        return Instantiate(obj, this.transform) as GameObject;
    }

}
