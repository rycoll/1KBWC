using UnityEngine;

public class EffectTabController : MonoBehaviour
{

    public GameObject introPanel;
    public GameObject questionPanel;
    public GameObject summaryPanel;

    public EffectBuilder builder;

    public void Start () {
        builder = new EffectBuilder();
    }

    public void OpenIntroPanel () {
        CloseAllPanels();
        introPanel.SetActive(true);
    }

    public void OpenQuestionPanel () {
        CloseAllPanels();
        questionPanel.SetActive(true);
    }

    public void OpenSummaryPanel () {
        CloseAllPanels();
        summaryPanel.SetActive(true);
    }

    public void CloseAllPanels () {
        introPanel.SetActive(false);
        questionPanel.SetActive(false);
        summaryPanel.SetActive(false);
    }

    public void Begin () {
        OpenQuestionPanel();
        QuestionPanelController question = questionPanel.GetComponent<QuestionPanelController>();
        builder = new EffectBuilder();
        question.SetEffectBuilder(builder);
        question.InitialState();
    }
}
