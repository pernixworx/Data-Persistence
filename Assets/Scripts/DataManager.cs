using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public string playerName; // new variable declared

    public InputField Field;

    public Text TextBox;

    public List<HighScoreEntry> highscoreEntryList;

    private string jsonString;


    [System.Serializable]
    public class SaveData
    {
        public List<HighScoreEntry> data;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        highscoreEntryList = new List<HighScoreEntry>()
        {
            new HighScoreEntry{score = 1, player = "A"},
            new HighScoreEntry{score = 2, player = "B"},
            new HighScoreEntry{score = 3, player = "A"}
        };
        highscoreEntryList = LoadScores();
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
        //SaveData data = new SaveData();
        //for (int i = 0; i < list.Count; i++)
        //{
        //    data.data.Add(list[i]);
        //}
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
            // SaveData data = JsonUtility.FromJson<SaveData>(jsonString);
            //for (int i = 0; i < Transforms.Count; i++)
            // {
            //     Transforms[i].localPosition = data.data[i];
            // }
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
        //for (int i = 0; i < highscoreEntryList.Count; i++)
        //{
        //    for (int j = 0; j < highscoreEntryList.Count; j++)
        //    {
        //        if (highscoreEntryList[j].score > highscoreEntryList[i].score)
        //        {
        //            HighScoreEntry tmp = highscoreEntryList[i];
        //            highscoreEntryList[i] = highscoreEntryList[j];
        //            highscoreEntryList[j] = tmp;
        //        }
        //    }
        //}
        highscoreEntryList.Sort();
        highscoreEntryList.Reverse();
    }
}