using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SubmittedEffectUI : MonoBehaviour
{
    [SerializeField]
    private Text rulesText = null;
    [SerializeField]
    private SummaryPanelController controller = null;

    public void SetRulesText(string text) {
        rulesText.text = text;
    }

    public void Delete() {
        controller.DeleteEffect(this.transform.GetSiblingIndex());
    }

}
