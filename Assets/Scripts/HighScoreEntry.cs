using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HighScoreEntry : System.IComparable<HighScoreEntry>
{
    public int score;
    public string player;

    public int CompareTo(HighScoreEntry item)
    {       // A null value means that this object is greater.
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
