using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

public class StartToSavePrincess : MonoBehaviour
{
    public GameObject player;
    public RawImage whiteImg;
    public RawImage videoImg;
    public VideoPlayer video;
    public string nextScene;
    public GameObject map;
    public Canvas canvas;

    private bool startWhiten = false;
    private bool whitenFinished = false;
    private bool playedVideo = false;

    private void Update()
    {
        //Debug.Log("video playing = " + video.isPlaying);
        //Debug.Log("Whitened = " + whitenFinished);
        //Debug.Log("Played video = " + playedVideo);
        if (whitenFinished && !video.isPlaying && !playedVideo)
        {
            StartCoroutine(PlayVideo());
        }

        if (whitenFinished && playedVideo && !video.isPlaying)
        {
            ChangeScene();
        }
    }

    public void MyPrincessDontWorry()
    {
        if (!startWhiten)
        {
            Debug.Log("StartSave!");
            startWhiten = true;
            StartCoroutine(GetWhitened());
        }
    }

    private IEnumerator GetWhitened()
    {
        float currAlpha = 0.1f;
        float targetAlpha = 1f;
        while (whiteImg.color.a < 0.99f)
        {
            currAlpha = Mathf.Lerp(currAlpha, targetAlpha, Time.deltaTime*currAlpha*currAlpha*10f);
            whiteImg.color = new Color(1f, 1f, 1f, currAlpha);

            yield return new WaitForSeconds(0.05f);
        }

        whitenFinished = true;
        
    }

    private IEnumerator PlayVideo()
    {
        
        map.SetActive(false);
        videoImg.color = new Color(1f, 1f, 1f, 1f);
        canvas.planeDistance = 1f;
        video.Play();
        yield return new WaitForSeconds(1f);
        whiteImg.color = new Color(1f, 1f, 1f, 0f);
        yield return new WaitForSeconds(3f);
        playedVideo = true;
    }

    private void ChangeScene()
    {
        Debug.Log("Change Scene!");
        GameObject.Destroy(player);
        SceneManager.LoadScene("maze");
    }


    public bool isWhitened
    {
        get
        {
            return whitenFinished;
        }
    }

}
