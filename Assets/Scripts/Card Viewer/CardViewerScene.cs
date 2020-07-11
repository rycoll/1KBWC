using UnityEngine;
using UnityEngine.SceneManagement;

public class CardViewerScene : MonoBehaviour {

    [SerializeField]
    private CardViewerUI viewerUI;

    public void Start () {
        viewerUI.LoadCards();
    }

    public void Exit () {
        SceneManager.LoadScene("title");
    }
}