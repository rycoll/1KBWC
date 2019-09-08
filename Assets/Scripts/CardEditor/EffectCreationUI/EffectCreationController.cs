using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCreationController : MonoBehaviour
{
    public GameObject returnModal;

    public void PromptReturnToMenu () {
        returnModal.SetActive(true);
    }

    public void CloseReturnPrompt () {
        returnModal.SetActive(false);
    }

    public void ReturnToMenu () {
        Application.Quit();
    }
}
