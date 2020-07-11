using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ApplyNewTagsToOldCards : MonoBehaviour, PostCreationStep
{
    [SerializeField]
    private PostCreationModalController controller;
    [SerializeField]
    private Text heading;
    [SerializeField]
    private CardViewerUI cardViewer;
    [SerializeField]
    private TagEditor tagEditor;
    private List<string> newTags;
    private int tagIndex = 0;
    private BinaryCardLoader cardLoader = new BinaryCardLoader();

    public void DoStep () {
        List<string> usedTags = tagEditor.GetUsedTags();
        List<string> existingTags = tagEditor.GetExistingTags();
        newTags = usedTags.Where(tag => !existingTags.Contains(tag)).ToList();
        tagIndex = 0;

        Next();
    }

    public void Next () {
        if (tagIndex < newTags.Count) {
            string currentTag = newTags[tagIndex];
            cardViewer.LoadCards();
            heading.text = 
                $"You have added a new tag: {currentTag.ToUpper()}\nSelect any existing cards that should also have this tag!";
        } else {
            controller.Next();
        }
    }

    public void End () {
        // apply new tag to each selected card
        List<CardData> selectedCards = cardViewer.GetSelectedCards();
        foreach (CardData card in selectedCards) {
            card.AddTag(newTags[tagIndex]);
        }
        cardLoader.SaveCards(selectedCards);
        tagIndex += 1;

        Next();
    }
}