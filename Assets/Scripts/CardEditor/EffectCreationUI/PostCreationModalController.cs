using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PostCreationModalController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> stepPanels;
    private int stepIndex = 0;

    private BinaryCardLoader binaryCardLoader = new BinaryCardLoader();
    private CardData card;

    public void Begin (CardData card) {
        stepIndex = 0;
        this.card = card;
        Next();
    }

    public void Next () {
        if (stepIndex >= stepPanels.Count) {
            Finish();
            return;
        }

        GameObject nextDisplay = stepPanels[stepIndex];
        stepIndex++;

        nextDisplay.SetActive(true);
        PostCreationStep step = nextDisplay.GetComponent<PostCreationStep>();
        step.DoStep();
    }

    private void Finish () {
        binaryCardLoader.SaveCard(card);
        SceneManager.LoadScene("title");
    }
}