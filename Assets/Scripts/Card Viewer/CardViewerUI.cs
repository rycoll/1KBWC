using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
    }

    public void LoadCard(CardData cardData) {
        Card card = new Card(cardData);
        GameObject display = Instantiate(cardDisplayPrefab, cardDisplayArea.transform) as GameObject;
        CardDisplaySmall displayInfo = display.GetComponent<CardDisplaySmall>();
        displayInfo.SetCard(card);
        displayInfo.SetSelectable(cardsAreSelectable);
    }

    public void ClearAllCards () {
        List<CardDisplaySmall> cards = GetCardDisplays();
        foreach (CardDisplaySmall card in cards) {
            Destroy(card.gameObject);
        }
    }

    public void LoadCards () {
        ClearAllCards();
        if (loadCardCallback == null) {
            loadCardCallback = LoadCard;
        }
        Debug.Log($"Loading cards with {loadCardCallback}");
        cardLoader.LoadCardsAsync(loadCardCallback);
    }

    public List<CardDisplaySmall> GetCardDisplays () {
        List<CardDisplaySmall> cards = new List<CardDisplaySmall>();
        foreach (Transform child in cardDisplayArea.transform) {
            CardDisplaySmall cardDisplay = child.gameObject.GetComponent<CardDisplaySmall>();
            if (cardDisplay) {
                cards.Add(cardDisplay);
            }
        }
        return cards;
    }

    public List<CardData> GetSelectedCards() {
        if (!cardsAreSelectable) {
            return new List<CardData>();
        }
        List<CardData> selectedCards = 
            GetCardDisplays()
            .Where(card => card.isSelected())
            .Select(card => card.card.GetData())
            .ToList();
        return selectedCards;
    }

}
