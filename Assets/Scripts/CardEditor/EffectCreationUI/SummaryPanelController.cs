using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SummaryPanelController : MonoBehaviour
{
    public Text rulesText;

    public void DisplayEffectSummary(List<byte> bytes) {
        RulesTextInterpreter RTI = new RulesTextInterpreter(bytes);
        string generatedRulesText = RTI.GetFullRulesText();
        rulesText.text = generatedRulesText;
    }

}
