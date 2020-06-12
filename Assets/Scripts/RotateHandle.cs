using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateHandle : MonoBehaviour
{
    public GameObject lightsObj;
    public float timeToAwakePrincess = 10f;
    public float initIntensity = 0.4f;
    public float finalIntensity = 4f;
    public GameObject princessSleep;
    public GameObject princessStand;
    public int winScore = 15;
    public RawImage winImage;
    public RawImage failImage;

    [HideInInspector]
    public static bool isBackToInitIntensity = false;
    public static bool isWin;
    public static int score;

    private int i;
    private float increaseIntensityPerFixedUpdate;
    private float decreaseIntensityPerFixedUpdate = 0.2f;
    private float currIntensity;
    private bool isMaxIntensity = false;
    private string scoreJsonPath = "./Assets/StreamingAssets/GameResult.json";

    private void Awake()
    {
        currIntensity = initIntensity;
        increaseIntensityPerFixedUpdate = (finalIntensity - initIntensity) / timeToAwakePrincess * Time.fixedDeltaTime;
        SetLightsIntensity(initIntensity);
        princessStand.SetActive(false);
        score = GetScore();
        isWin = (score >= winScore);
        winImage.enabled = false;
        failImage.enabled = false;
    }

    private void FixedUpdate()
    {
        if (HoldHandle.isRotate && !isMaxIntensity)
        {
            currIntensity += increaseIntensityPerFixedUpdate;
            SetLightsIntensity(currIntensity);
            if (currIntensity > finalIntensity)
            {
                isMaxIntensity = true;
                JudgePrincessFate();
                StartCoroutine(BackToInitIntensity());
            }
        }
    }

    private void SetLightsIntensity(float intensity)
    {
        Transform lightObj;
        Light light;
        for (i = 0; i < lightsObj.transform.childCount; i++)
        {
            lightObj = lightsObj.transform.GetChild(i);
            light = lightObj.GetComponent<Light>();
            light.intensity = intensity;
        }
    }


    private IEnumerator BackToInitIntensity()
    {
        while (currIntensity > initIntensity)
        {
            currIntensity -= decreaseIntensityPerFixedUpdate;
            SetLightsIntensity(currIntensity);
            yield return new WaitForSeconds(0.1f);
        }

        SetLightsIntensity(initIntensity);
        isBackToInitIntensity = true;
    }

    private int GetScore()
    {
        return ReadJSON.Read(scoreJsonPath).gameResult;
    }

    private void JudgePrincessFate()
    {
        if (isWin)
        {
            princessSleep.SetActive(false);
            princessStand.SetActive(true);
            winImage.enabled = true;
        }
        else
        {
            failImage.enabled = true;
        }
    }
}
