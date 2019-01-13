using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class BinaryCardLoader : MonoBehaviour {
    const string FOLDER_NAME = "SavedCards";
    const string FILE_EXTENSION = ".bwc";

    public void SaveCards (List<Card> cards) {
        //string folderPath = Path.Combine(Application.persistentDataPath, FOLDER_NAME);
        string folderPath = FOLDER_NAME;
        if (!Directory.Exists(folderPath)) {
            Directory.CreateDirectory(folderPath);
        }
        
        foreach (Card card in cards) {
            string dataPath = Path.Combine(folderPath, card.cardName + FILE_EXTENSION);
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fileStream = File.Open(dataPath, FileMode.OpenOrCreate)) {
                bf.Serialize(fileStream, card);
            }
        }
    }

    public List<Card> LoadCards () {
        //string folderPath = Path.Combine(Application.persistentDataPath, FOLDER_NAME);
        string folderPath = FOLDER_NAME;
        string[] filePaths = Directory.GetFiles(folderPath, "*" + FILE_EXTENSION);

        List<Card> cards = new List<Card>();
        foreach (string dataPath in filePaths) {
            Debug.Log(dataPath);
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fileStream = File.Open(dataPath, FileMode.Open)) {
                cards.Add((Card) bf.Deserialize(fileStream));
            }
        }

        return cards;
    }
}