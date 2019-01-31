using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddEffectUI : MonoBehaviour
{
    public Dropdown dropdown;
    public GameObject effectPanelPrefab;

    public void Start () {
        SetOptions();
    }

    public void AddEffect () {
        GameObject effectObj = Instantiate(effectPanelPrefab) as GameObject;
        GameObject addButton = Instantiate(gameObject) as GameObject;
        effectObj.transform.SetParent(transform.parent);
        //effectObj.transform.SetSiblingIndex(transform.parent.childCount - 2);
        addButton.transform.SetParent(effectObj.transform);
        effectObj.GetComponent<Image>().color =  Random.ColorHSV();

    }

    private void SetOptions () {

    }
}
