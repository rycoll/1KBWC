using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddEffectUI : MonoBehaviour
{
    public Dropdown dropdown;
    public GameObject effectPanelPrefab;
    public GameObject addButtonPrefab;

    public void Start () {
        SetOptions();
    }

    public void AddEffect () {
        GameObject effectObj = Instantiate(effectPanelPrefab) as GameObject;
        GameObject addButton = Instantiate(addButtonPrefab) as GameObject;
        addButton.transform.SetParent(effectObj.transform);
        effectObj.transform.SetParent(transform.parent);
        effectObj.transform.SetSiblingIndex(transform.parent.childCount - 2);
        effectObj.GetComponent<Image>().color =  Random.ColorHSV();

    }

    private void SetOptions () {

    }
}
