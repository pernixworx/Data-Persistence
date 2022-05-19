using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{

    private Transform entryContainer;
    private Transform entryTemplate;
    public List<Transform> highscoreEntryTransformList;

    public static HighScoreTable Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
     
        entryContainer = transform.Find("HighScoreFieldContainer");
        entryTemplate = entryContainer.Find("HighScoreField");
    }

    private void CreateHighScoreEntryTransform(HighScoreEntry entry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 20f;
        Transform entryTransform = Instantiate(entryTemplate, entryContainer);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int score = entry.score;
        Debug.Log("score set to " + score);
        entryTransform.Find("highScoreNumber").GetComponent<Text>().text = score.ToString();

        string player = entry.player;
        Debug.Log("name set to " + player);
        entryTransform.Find("highScoreName").GetComponent<Text>().text = player;

        transformList.Add(entryTransform);
    }


    public void PrintScores()
    {
        int scoreField = 8;
        foreach (HighScoreEntry item in DataManager.Instance.highscoreEntryList)
        {
            scoreField -= 1;
            CreateHighScoreEntryTransform(item, entryContainer, highscoreEntryTransformList);
            if (scoreField == 0)
            {
                return;
            }
        }
    }
}