using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabInterface : MonoBehaviour
{
    public GameObject tabButtonContainer;
    public Color inactiveColour;
    public Color activeColour;

    private void Start () {
        GetComponent<Image>().color = activeColour;
        TabButton.activeColour = activeColour;
        SetTabColoursInactive();
    }

    public void SetChildrenInactive () {
        foreach (Transform child in transform) {
            child.gameObject.SetActive(false);
        }
    }

    public void SetTabColoursInactive () {
        foreach (Transform child in tabButtonContainer.transform) {
            child.gameObject.GetComponent<Image>().color = inactiveColour;
        }
    }
}
