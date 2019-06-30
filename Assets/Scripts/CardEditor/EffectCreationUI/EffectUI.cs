using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectUI : MonoBehaviour
{
    public GameObject addEffectButton;
    public GameObject fieldPanelPrefab;
    public EffectInfoUI infoDiv;

    private EffectNode effect;
    private EffectData effectData;

    public void Awake () {
        effect = GetComponent<EffectNode>();
    }

    public void SetEffect (EffectData d) {
        effect.SetData(d);
        Open();
    }

    private void Open () {
        effectData = effect.GetEffectData();
        DisplayInfo();
        addEffectButton.SetActive(effectData.takesSubEffects);

        foreach (FieldData fieldData in effectData.fields) {
            GameObject fieldObj = Instantiate(fieldPanelPrefab, this.transform) as GameObject;
            FieldUI fieldUI = fieldObj.GetComponent<FieldUI>();
            fieldUI.SetData(fieldData);
        }
    }

    public void DisplayInfo () {
        infoDiv.UpdateDisplay(effectData);
    }
}
