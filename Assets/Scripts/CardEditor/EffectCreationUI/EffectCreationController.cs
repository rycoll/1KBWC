using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCreationController : MonoBehaviour
{
    [SerializeField]
    private GameObject returnModal = null;
    [SerializeField]
    private ErrorModal errorModal = null;

    public void PromptReturnToMenu () {
        returnModal.SetActive(true);
    }

    public void ReportError(string errorMessage) {
        errorModal.ShowError(errorMessage);
    }

    public void CloseReturnPrompt () {
        returnModal.SetActive(false);
    }

    public void ReturnToMenu () {
        Application.Quit();
    }
}
