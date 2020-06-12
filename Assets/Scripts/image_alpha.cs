using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class image_alpha : MonoBehaviour
{
    public int close;
    public GameObject toDestroy;
    
    AudioPlayer audioP;
    GameObject toDestroy2;

    // Start is called before the first frame update
    void Start()
    {
        close = 0;
        audioP = GameObject.Find("AudioPlayer").GetComponent<AudioPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (close == 0)
        {
            var tempColor = this.GetComponent<Image>().color;
            tempColor.a -= (float)(0.1 * Time.deltaTime);
            this.GetComponent<Image>().color = tempColor;
            if(tempColor.a <= 0)
            {
                close = 2;
            }
        }
        else if(close == 1)
        {
            var tempColor = this.GetComponent<Image>().color;
            tempColor.a += (float)(0.5 * Time.deltaTime);
            this.GetComponent<Image>().color = tempColor;
            if(tempColor.a >= 1)
            {
                toDestroy2 = GameObject.Find("[SteamVR]");
                GameObject.Destroy(toDestroy);
                GameObject.Destroy(toDestroy2);
                SceneManager.LoadScene(2);
            }
        }
    }
}
