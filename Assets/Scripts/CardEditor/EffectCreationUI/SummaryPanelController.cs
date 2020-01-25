using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SummaryPanelController : MonoBehaviour
{
    [SerializeField]
    private EffectTabController controller;
    [SerializeField]
    private List<SubmittedEffectUI> submittedEffectUIs;

    private const int MAX_EFFECTS_PER_CARD = 5;

    public void DisplayEffectSummary(List<List<byte>> effectList) {
        // clear existing submitted effect UIs
        foreach(SubmittedEffectUI effectUI in submittedEffectUIs) {
            effectUI.gameObject.SetActive(false);
        }

        for (int i = 0; i < effectList.Count && i < MAX_EFFECTS_PER_CARD; i++) {
            RulesTextInterpreter RTI = new RulesTextInterpreter(effectList[i]);
            string generatedRulesText = RTI.GetFullRulesText();
            SubmittedEffectUI effectUI = submittedEffectUIs[MAX_EFFECTS_PER_CARD - 1 - i];
            effectUI.SetRulesText(generatedRulesText);
            effectUI.gameObject.SetActive(true);
        }
    }

    public void DeleteEffect(int index) {
        controller.DeleteEffect(index);
    }

    public void AddAnotherEffect() {
        if (controller.GetNumberOfEffects() < MAX_EFFECTS_PER_CARD) {
            controller.Begin();
        }
    }

}
