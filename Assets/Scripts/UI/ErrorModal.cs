using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorModal : MonoBehaviour
{
    [SerializeField]
    private Text errorText;

    public void ShowError(string errorMessage) {
        this.gameObject.SetActive(true);
        errorText.text = errorMessage;
    }

    public void Close() {
        this.gameObject.SetActive(false);
    }
}
