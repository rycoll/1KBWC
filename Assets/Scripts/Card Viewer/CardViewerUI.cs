using UnityEngine;
using UnityEngine.SceneManagement;

public class CardViewerUI : MonoBehaviour
{

    private LoadCardCallback loadCardCallback;
    [SerializeField]
    private GameObject cardDisplayPrefab;
    [SerializeField]
    private GameObject cardDisplayArea;

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
    }

    public void LoadCards () {
        cardLoader.LoadCardsAsync(loadCardCallback);
    }

    public void ReturnToTitle() {
        SceneManager.LoadScene("title");
    }

}

public delegate void LoadCardCallback(CardData card);
