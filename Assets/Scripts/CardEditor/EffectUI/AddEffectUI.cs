﻿using System.Collections;
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
        GameObject effectObj = Instantiate(effectPanelPrefab, transform.parent) as GameObject;
        GameObject addButton = Instantiate(gameObject, effectObj.transform) as GameObject;
        effectObj.transform.SetSiblingIndex(transform.parent.childCount - 2);
        effectObj.GetComponent<Image>().color =  Random.ColorHSV();

    }

    private void SetOptions () {

    }
}
