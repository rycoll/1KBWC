using UnityEngine;
using UnityEngine.SceneManagement;

public class CardCreationController : MonoBehaviour
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
        SceneManager.LoadScene("title");
    }
}
