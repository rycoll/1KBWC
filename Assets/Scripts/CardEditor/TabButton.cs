using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabButton : MonoBehaviour
{
    public GameObject tab;
    public static Color activeColour;

    public void OpenTab () {
        tab.SetActive(true);
        GetComponent<Image>().color = activeColour;
        tab.GetComponent<Image>().color = activeColour;
    }
}
