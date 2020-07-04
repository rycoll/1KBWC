using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardViewerUI : MonoBehaviour
{

    private LoadCardCallback loadCardCallback;
    [SerializeField]
    private GameObject cardDisplayPrefab;
    [SerializeField]
    private GameObject cardDisplayArea;
    [SerializeField]
    private bool cardsAreSelectable = false;

    BinaryCardLoader cardLoader = new BinaryCardLoader();

    public void Start () {
        loadCardCallback = LoadCard;
        LoadCards();
    }

    public void LoadCard(CardData cardData) {
        Card card = new Card(cardData);
        GameObject display = Instantiate(cardDisplayPrefab, cardDisplayArea.transform) as GameObject;
        CardDisplaySmall displayInfo = display.GetComponent<CardDisplaySmall>();
        displayInfo.SetCard(card);
        displayInfo.SetSelectable(cardsAreSelectable);
    }

    public void LoadCards () {
        cardLoader.LoadCardsAsync(loadCardCallback);
    }

    public void ReturnToTitle() {
        SceneManager.LoadScene("title");
    }

    public List<CardData> GetSelectedCards() {
        if (!cardsAreSelectable) {
            return new List<CardData>();
        }
        List<CardData> selectedCards = new List<CardData>();
        foreach (Transform child in cardDisplayArea.transform) {
            CardDisplaySmall cardDisplay = child.gameObject.GetComponent<CardDisplaySmall>();
            if (cardDisplay && cardDisplay.isSelected()) {
                Card card = cardDisplay.GetCard();
                if (card != null) {
                    CardData cardData = card.GetData();
                    selectedCards.Add(cardData);
                }
            }
        }

        return selectedCards;
    }

}
