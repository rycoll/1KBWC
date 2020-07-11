using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class BinaryCardLoader {
    const string FOLDER_NAME = "SavedCards";
    const string FILE_EXTENSION = ".bwc";
    const string IMAGE_FORMAT = ".png";

    public string GetCardPath(string cardName) {
        string folderPath = FOLDER_NAME;
        if (!Directory.Exists(folderPath)) {
            Directory.CreateDirectory(folderPath);
        }
        string directory = Path.Combine(folderPath, cardName);
        if (!Directory.Exists(directory)) {
            Directory.CreateDirectory(directory);
        }
        return Path.Combine(directory, cardName + FILE_EXTENSION);
    }
    public string GetImagePath(string cardName, string cardID) {
        string folderPath = FOLDER_NAME;
        if (!Directory.Exists(folderPath)) {
            Directory.CreateDirectory(folderPath);
        }
        string directory = Path.Combine(folderPath, cardName);
        if (!Directory.Exists(directory)) {
            Directory.CreateDirectory(directory);
        }
        return Path.Combine(directory, cardID + IMAGE_FORMAT);
    }

    public void SaveCard (CardData card) {
        string path = GetCardPath(card.Name);
        BinaryFormatter bf = new BinaryFormatter();
        using (FileStream fileStream = File.Open(path, FileMode.OpenOrCreate)) {
            bf.Serialize(fileStream, card);
        }
        byte[] imageBytes = card.ArtTexture.EncodeToPNG();
        string imagePath = GetImagePath(card.Name, card.GetID());
        File.WriteAllBytes(imagePath, imageBytes);
    }

    public void SaveCards (List<CardData> cards) {
        foreach (CardData card in cards) {
            SaveCard(card);
        }
    }

    public CardData LoadCardData (string path) {
        BinaryFormatter bf = new BinaryFormatter();
        CardData cardData;
        using (FileStream fileStream = File.Open(path, FileMode.Open)) {
            cardData = (CardData) bf.Deserialize(fileStream);
        }
        return cardData;
    }

    public List<CardData> LoadCards () {
        string folderPath = FOLDER_NAME;
        string[] filePaths = Directory.GetFiles(folderPath, "*" + FILE_EXTENSION, SearchOption.AllDirectories);

        List<CardData> cards = new List<CardData>();
        foreach (string dataPath in filePaths) {
            CardData cardData = LoadCardData(dataPath);
            if (cardData != null) {
                cards.Add(cardData);
                string dirName = Path.GetDirectoryName(dataPath);
                byte[] imageBytes = File.ReadAllBytes(Path.Combine(dirName, cardData.GetID() + IMAGE_FORMAT));
                cardData.ArtTexture = new Texture2D(300, 300, TextureFormat.RGBA32, false, false);
                cardData.ArtTexture.LoadImage(imageBytes, false);
                cardData.ArtTexture.Apply();
            }
        }

        return cards;
    }

    public void LoadCardsAsync (LoadCardCallback callback) {
        string folderPath = FOLDER_NAME;
        string[] filePaths = Directory.GetFiles(folderPath, "*" + FILE_EXTENSION, SearchOption.AllDirectories);

        List<CardData> cards = new List<CardData>();
        foreach (string dataPath in filePaths) {
            CardData cardData = LoadCardData(dataPath);
            if (cardData != null) {
                cards.Add(cardData);
                string dirName = Path.GetDirectoryName(dataPath);
                byte[] imageBytes = File.ReadAllBytes(Path.Combine(dirName, cardData.GetID() + IMAGE_FORMAT));
                cardData.ArtTexture = new Texture2D(300, 300, TextureFormat.RGBA32, false, false);
                cardData.ArtTexture.LoadImage(imageBytes, false);
                cardData.ArtTexture.Apply();

                callback(cardData);
            }
        }
    }

    // return true if successful, false if failed
    public void UpdateCardData (CardData newData) {
        // string path = GetCardPath(newData.Name);
        // CardData existingData = LoadCardData(path);
        // if (existingData == null) return false;  

        // return true;
        SaveCard(newData);
    }
}

public delegate void LoadCardCallback(CardData card);