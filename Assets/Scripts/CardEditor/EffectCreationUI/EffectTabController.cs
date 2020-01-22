using UnityEngine;
using System.Collections.Generic;

public class EffectTabController : MonoBehaviour
{

    public GameObject introPanel;
    public GameObject questionPanel;
    public GameObject summaryPanel;

    private List<List<byte>> builtEffects = new List<List<byte>>();

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
        SummaryPanelController summary = summaryPanel.GetComponent<SummaryPanelController>();
        summary.DisplayEffectSummary(GetConcatenatedEffects());
    }

    public void CloseAllPanels () {
        introPanel.SetActive(false);
        questionPanel.SetActive(false);
        summaryPanel.SetActive(false);
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
        summary.DisplayEffectSummary(GetConcatenatedEffects());
    }

    public List<byte> GetConcatenatedEffects () {
        List<byte> bytes = new List<byte>();
        foreach (List<byte> list in builtEffects) {
            bytes.AddRange(new List<byte>(list.ToArray()));
            bytes.Add((byte) Instruction.EFFECT_DELIMITER);
        }
        return bytes;
    }

}
