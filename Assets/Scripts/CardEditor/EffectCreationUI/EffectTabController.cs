using UnityEngine;
using System.Collections.Generic;

public class EffectTabController : MonoBehaviour
{

    [SerializeField]
    private GameObject introPanel = null;
    [SerializeField]
    private GameObject questionPanel = null;
    [SerializeField]
    private GameObject summaryPanel = null;
    [SerializeField]
    private CardCreationController parentController = null;

    private List<List<byte>> builtEffects = new List<List<byte>>();

    public void OpenIntroPanel () {
        introPanel.SetActive(true);
    }

    public void OpenQuestionPanel () {
        questionPanel.SetActive(true);
    }

    public void ReportError(string errorMessage) {
        OpenSummaryPanel();
        parentController.ReportError(errorMessage);
    }

    public void OpenSummaryPanel () {
        summaryPanel.SetActive(true);
        SummaryPanelController summary = summaryPanel.GetComponent<SummaryPanelController>();
        summary.DisplayEffectSummary(builtEffects);
    }

    public void Begin () {
        OpenQuestionPanel();
        QuestionPanelController question = questionPanel.GetComponent<QuestionPanelController>();
        question.InitialState();
    }

    public void AddEffect (List<byte> effect) {
        builtEffects.Insert(0, effect);
    }

    public void DeleteEffect (int index) {
        builtEffects.RemoveAt(index);
        SummaryPanelController summary = summaryPanel.GetComponent<SummaryPanelController>();
        summary.DisplayEffectSummary(builtEffects);
    }

    public List<byte> GetConcatenatedEffects () {
        List<byte> bytes = new List<byte>();
        foreach (List<byte> list in builtEffects) {
            bytes.AddRange(new List<byte>(list.ToArray()));
            bytes.Add((byte) Instruction.EFFECT_DELIMITER);
        }
        return bytes;
    }

    public int GetNumberOfEffects () {
        return builtEffects.Count;
    }

}
