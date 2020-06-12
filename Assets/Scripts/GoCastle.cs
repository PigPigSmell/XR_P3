using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GoCastle : MonoBehaviour
{
    image_alpha img;
    GetMagic gm;
    HitSth hsth;

    public int score;

    // Start is called before the first frame update
    void Start()
    {
        img = GameObject.Find("Image").GetComponent<image_alpha>();
        //gm = GameObject.Find("right").GetComponent<GetMagic>();
        gm = GameObject.Find("Controller (right)").GetComponent<GetMagic>();
        hsth = GameObject.Find("note").GetComponent<HitSth>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "soul")
        {
            int num = gm.GetMagicNum();
            score = (int)(num / 3);
            score += hsth.SentScore();
            img.close = 1;

            WriteData();
        }
    }

    public int Score()
    {
        return score;
    }

    public void WriteData()
    {
        Variables var = new Variables();

        var.gameResult = score;

        string saveString = JsonUtility.ToJson(var);

        //write to Josn
        StreamWriter file = new StreamWriter(System.IO.Path.Combine(Application.streamingAssetsPath, "GameResult.json"));
        file.Write(saveString);
        file.Close();
    }

    [System.Serializable]
    public class Variables
    {
        // GamePanel.cs
        public int gameResult;
    }

}


