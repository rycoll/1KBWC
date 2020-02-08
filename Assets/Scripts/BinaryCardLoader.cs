using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class BinaryCardLoader {
    const string FOLDER_NAME = "SavedCards";
    const string FILE_EXTENSION = ".bwc";
    const string IMAGE_FORMAT = ".png";

    public void SaveCards (List<CardData> cards) {
        string folderPath = FOLDER_NAME;
        if (!Directory.Exists(folderPath)) {
            Directory.CreateDirectory(folderPath);
        }
        
        foreach (CardData card in cards) {
            string directory = Path.Combine(folderPath, card.Name);
            if (!Directory.Exists(directory)) {
                Directory.CreateDirectory(directory);
            }
            Debug.Log(directory);
            string dataPath = Path.Combine(directory, card.Name + FILE_EXTENSION);
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fileStream = File.Open(dataPath, FileMode.OpenOrCreate)) {
                bf.Serialize(fileStream, card);
            }

            byte[] imageBytes = card.ArtTexture.EncodeToPNG();
            
            string imagePath = Path.Combine(directory, card.GetID() + IMAGE_FORMAT);
            File.WriteAllBytes(imagePath, imageBytes);
        }
    }

    public List<CardData> LoadCards () {
        string folderPath = FOLDER_NAME;
        string[] filePaths = Directory.GetFiles(folderPath, "*" + FILE_EXTENSION, SearchOption.AllDirectories);

        List<CardData> cards = new List<CardData>();
        foreach (string dataPath in filePaths) {
            BinaryFormatter bf = new BinaryFormatter();
            CardData cardData;
            using (FileStream fileStream = File.Open(dataPath, FileMode.Open)) {
                cardData = (CardData) bf.Deserialize(fileStream);
            }
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
            BinaryFormatter bf = new BinaryFormatter();
            CardData cardData;
            using (FileStream fileStream = File.Open(dataPath, FileMode.Open)) {
                cardData = (CardData) bf.Deserialize(fileStream);
            }
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
}