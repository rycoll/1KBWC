using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldUI : MonoBehaviour
{

    public Text headerText;
    public GameObject toggleButton;

    private FieldData data;

    public GameObject stringFieldPrefab;
    public GameObject numberFieldPrefab;
    public GameObject dropdownPrefab;

    private GameObject currEnterField;
    private GameObject currSelectField;
    private GameObject currChild;

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
        foreach (QueryData queryData in data.queryDropdown) {
            options.Add(queryData.name);
        }
        dropdown.AddOptions(options);
        dropdownObj.SetActive(false);

        switch (data.enterValue) {
            case EnterValueType.BOOL:
                break;
            case EnterValueType.NONE:
                toggleButton.SetActive(false);
                dropdownObj.SetActive(true);
                break;
            case EnterValueType.NUMBER:
                currEnterField = AddPrefabToDisplay(this.numberFieldPrefab);
                break;
            case EnterValueType.TEXT:
                currEnterField = AddPrefabToDisplay(this.stringFieldPrefab);
                break;
        }

        currSelectField = dropdownObj;
    }

    public void Toggle () {
        if (data.enterValue == EnterValueType.NONE) {
            currSelectField.SetActive(true);
            return;
        }
        if (currEnterField.activeSelf) {
            currEnterField.SetActive(false);
            currSelectField.SetActive(true);
        } else {
            currEnterField.SetActive(true);
            currSelectField.SetActive(false);
        }
    }

    public void SetSubQuery (QueryData q) {
        if (this.currChild) {
            Destroy(currChild);
        }
        currChild = Instantiate(this, this.transform) as GameObject;
        currChild.GetComponent<FieldUI>().SetData(q);
    }

    public GameObject AddPrefabToDisplay (GameObject obj) {
        return Instantiate(obj, this.transform) as GameObject;
    }

}
