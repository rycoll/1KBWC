using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenUIController : MonoBehaviour
{

    public void OpenCardCreation() {
        SceneManager.LoadScene("card-creation");
    }

    public void OpenCardViewer() {
        SceneManager.LoadScene("card-viewer");
    }
}
