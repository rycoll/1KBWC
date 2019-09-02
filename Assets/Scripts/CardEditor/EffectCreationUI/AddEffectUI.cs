using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AddEffectUI : MonoBehaviour
{
    public Dropdown dropdown;
    public GameObject effectPanelPrefab;

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
        effectObj.GetComponent<Image>().color = GetRandColorThatContrastsWithBlack();

        EffectData data = EffectData.GetEffectDataByName(
            dropdown.options[dropdown.value].text
        );
        
        //EffectUI effectUI = effectObj.GetComponent<EffectUI>();
        //effectUI.SetEffect(data);
    }

    private void SetOptions () {
        List<string> options = EffectData.InstructionDataMap.Values
            .Where(value => EffectData.BaseInstructions.Contains(value.instruction))
            .Select(value => value.name).ToList();
        dropdown.ClearOptions();
        dropdown.AddOptions(options);
    }
}
