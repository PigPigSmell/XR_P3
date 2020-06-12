using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class PressStartButton : MonoBehaviour
{
    public Button startButton;

    private SteamVR_Behaviour_Boolean behaviour_Boolean = new SteamVR_Behaviour_Boolean();
    private bool start = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "StartButton")
        {
            start = SteamVR_Input.GetState("LookLetter", "PressStart", behaviour_Boolean.inputSource);
            if (start)
            {
                startButton.onClick.Invoke();
            }
        }
    }

}
