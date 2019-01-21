using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TagEditor : MonoBehaviour
{
    public GameObject activeTagPanel;
    public GameObject existingTagPanel;
    public GameObject tagPrefab;

    public Text newTagField;
    public Text newTagErrorText;

    private List<string> tags;
    private List<string> unusedTags;

    public void Start () {
        tags = new List<string>();
        unusedTags = new List<string>();
        AddUnusedTag("Cheese");
        AddUnusedTag("Avocado");
        AddUnusedTag("Stench");
    }

    public void AddNewTag () {
        string newTag = newTagField.text;
        newTagField.text = "";

        if (newTag.Equals("")) {
            newTagErrorText.text = "Enter a tag";
            return;
        }
        if (tags.Contains(newTag)) {
            newTagErrorText.text = "The card already has this tag!";
            return;
        }
        if (unusedTags.Contains(newTag)) {
            //RemoveUnusedTag(newTag);
        } 
        AddTag(newTag);
    }

    public void AddTag (string str) {
        GameObject tagDisplay = Instantiate(tagPrefab) as GameObject;
        tagDisplay.transform.SetParent(activeTagPanel.transform);
        tagDisplay.GetComponent<TagItemDisplay>().SetTag(str);
        Button button = tagDisplay.GetComponent<Button>();
        button.onClick.AddListener(delegate{RemoveTag(tagDisplay);});
        tags.Add(str);
    }

    public List<string> GetTags () {
        return tags;
    }

    public void RemoveTag (GameObject tagObject) {
        TagItemDisplay display = tagObject.GetComponent<TagItemDisplay>();
        tags.Remove(display.GetTag());
        Destroy(tagObject);
    }

    public void AddUnusedTag(string str) {
        if (!unusedTags.Contains(str)) {
            GameObject tagDisplay = Instantiate(tagPrefab) as GameObject;
            tagDisplay.transform.SetParent(existingTagPanel.transform);
            tagDisplay.GetComponent<TagItemDisplay>().SetTag(str);
            Button button = tagDisplay.GetComponent<Button>();
            button.onClick.AddListener(delegate{UseExistingTag(tagDisplay);});
            unusedTags.Add(str);
        }
    }

    public void UseExistingTag(GameObject tagObject) {
        string newTag = tagObject.GetComponent<TagItemDisplay>().GetTag();
        if (!tags.Contains(newTag)) {
            AddTag(newTag);
        }
        //RemoveUnusedTag(tagObject);
    }

    public void RemoveUnusedTag (GameObject tagObject) {
        TagItemDisplay display = tagObject.GetComponent<TagItemDisplay>();
        unusedTags.Remove(display.GetTag());
        Destroy(tagObject);
    }

    public void RemoveUnusedTag (string tag) {
        foreach (Transform child in existingTagPanel.transform) {
            string item = child.GetComponent<TagItemDisplay>().GetTag();
            if (item.Equals(tag)) {
                RemoveUnusedTag(child.gameObject);
            }
        }
    }

}
