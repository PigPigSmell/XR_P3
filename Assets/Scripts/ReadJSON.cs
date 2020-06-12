using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ReadJSON : MonoBehaviour
{
    public static Score Read(string path)
    {
        string loadData = File.ReadAllText("./Assets/StreamingAssets/GameResult.json");
        Score score = JsonUtility.FromJson<Score>(loadData);

        return score;
    }
}

public class Score
{
    public int gameResult;
}
