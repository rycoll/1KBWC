using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddEffectUI : MonoBehaviour
{
    public Dropdown dropdown;
    public GameObject effectPanelPrefab;
    public GameObject infoPanelPrefab;

    public void Start () {
        SetOptions();
    }

    public void AddEffect () {
        GameObject effectObj = Instantiate(effectPanelPrefab, transform.parent) as GameObject;
        effectObj.transform.SetSiblingIndex(transform.parent.childCount - 2);

        GameObject infoObj = Instantiate(infoPanelPrefab, effectObj.transform) as GameObject;
        GameObject addButton = Instantiate(gameObject, effectObj.transform) as GameObject;
        
        effectObj.GetComponent<Image>().color =  Random.ColorHSV();
    }

    private void SetOptions () {

    }
}
