using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public string playerName;

    public InputField Field;

    public Text TextBox;

    public List<HighScoreEntry> highscoreEntryList;

    private string jsonString;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        highscoreEntryList = LoadScores();
    }

    public void NewName()
    {
        // add code here to handle when a color is selected
        TextBox.text = Field.text;
        DataManager.Instance.playerName = TextBox.text;
        Debug.Log("name set to " + playerName);
    }

    public void SaveScores(List<HighScoreEntry> list)
    {
        string json = JsonConvert.SerializeObject(list);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        highscoreEntryList = list;
    }
    
    public List<HighScoreEntry> LoadScores()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            jsonString = File.ReadAllText(path); // read the json file from the file system
            List<HighScoreEntry> myData = JsonConvert.DeserializeObject<List<HighScoreEntry>>(jsonString); // de-serialize the data to your myData object
            return myData;

        }
        else
        {
            string json = JsonUtility.ToJson(highscoreEntryList);
            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
            jsonString = File.ReadAllText(path); // read the json file from the file system
            List<HighScoreEntry> myData = JsonConvert.DeserializeObject<List<HighScoreEntry>>(jsonString); // de-serialize the data to your myData object
            return myData;
        }

    }

    public List<HighScoreEntry> AppendScore(string playerName, int scoreNumber, List<HighScoreEntry> scoreList)
    {
        HighScoreEntry newScore = new HighScoreEntry
        {
            player = playerName,
            score = scoreNumber
        };
        scoreList.Add(newScore);
        return scoreList;
    }

    public void SortScores()
    {
        highscoreEntryList.Sort();
        highscoreEntryList.Reverse();
    }
}