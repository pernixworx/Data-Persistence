using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HighScoreEntry : System.IComparable<HighScoreEntry>
{
    public int score;
    public string player;

    public int CompareTo(HighScoreEntry item)
    {       
        if (item == null)
        {
            return 1;
        }
        else
        {
            return this.score.CompareTo(item.score);
        }
    }
}
