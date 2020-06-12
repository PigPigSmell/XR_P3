using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadLetter : MonoBehaviour
{
    public Text text;
    public Button startButton;
    public string stringToShow;

    private int strLen;
    private int currLen = 0;
    private bool isShowing = false;
    private static bool allShowed = false;

    private void Awake()
    {
        strLen = stringToShow.Length;
    }

    private void Update()
    {
        if (!isShowing)
        {
            if (TakeLetter.isTaking)
            {
                StartCoroutine(ShowText());
            }
        }
    }

    private IEnumerator ShowText()
    {
        while (currLen < strLen)
        {
            currLen++;
            isShowing = true;
            text.text = stringToShow.Substring(0, currLen);
            yield return new WaitForSeconds(0.1f);
        }

        isShowing = false;
        allShowed = true;
        startButton.gameObject.SetActive(true);

    }

    public static bool isTextAllShowed
    {
        get
        {
            return allShowed;
        }
    }

}
