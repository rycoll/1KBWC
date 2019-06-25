using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AddEffectUI : MonoBehaviour
{
    public Dropdown dropdown;
    public GameObject effectPanelPrefab;
    public GameObject infoPanelPrefab;
    public GameObject fieldPanelPrefab;

    public GameObject inputPlayerPrefab;

    public void Start () {
        SetOptions();
    }

    public Color GetRandColorThatContrastsWithBlack () {
        Color c;
        do {
            c = Random.ColorHSV();
        } while ((c.r*0.299 + c.g*0.587 + c.b*0.114) < 0.5);
        return c;
    }

    public void AddEffect () {
        GameObject effectObj = Instantiate(effectPanelPrefab, transform.parent) as GameObject;
        effectObj.transform.SetSiblingIndex(transform.parent.childCount - 2);

        GameObject infoObj = Instantiate(infoPanelPrefab, effectObj.transform) as GameObject;
        
        effectObj.GetComponent<Image>().color = GetRandColorThatContrastsWithBlack();

        // todo : get correct data for the instruction, by name (dropdown ui value string)

        EffectData data = EffectData.GetEffectDataByName(
            dropdown.options[dropdown.value].text
        );

        // todo : instatiate and position an appropriate UI for the effect data

        // todo : make sure fields are properly added

        // old code

        foreach (FieldData fieldData in data.fields) {
            GameObject fieldObj = Instantiate(fieldPanelPrefab, effectObj.transform) as GameObject;
            FieldUI fieldUI = fieldObj.GetComponent<FieldUI>();

            fieldUI.SetData(fieldData);
        }

        if (data.takesSubEffects) {
            GameObject addButton = Instantiate(gameObject, effectObj.transform) as GameObject;
        }

        EffectInfoUI infoUI = infoObj.GetComponent<EffectInfoUI>();
        if (infoUI) {
            infoUI.UpdateDisplay(data);
        }
    }

    private void SetOptions () {
        List<string> options = EffectData.InstructionDataMap.Values
            .Where(value => EffectData.BaseInstructions.Contains(value.instruction))
            .Select(value => value.name).ToList();
        dropdown.ClearOptions();
        dropdown.AddOptions(options);
    }
}
