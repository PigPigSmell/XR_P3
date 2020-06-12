using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TakeLetter : MonoBehaviour
{
    public GameObject letter;
    public GameObject gameAction;

    private SteamVR_Behaviour_Boolean behaviour_Boolean = new SteamVR_Behaviour_Boolean();
    private bool take = false;
    private Vector3 letterOriginPosition;
    private Quaternion letterOriginRotation;

    private static bool taking = false;


    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Letter")
        {
            take = SteamVR_Input.GetState("LookLetter", "Take", behaviour_Boolean.inputSource);
            if (take)
            {
                gameAction.GetComponent<StartToSavePrincess>().MyPrincessDontWorry();
                taking = true;
            }
            else if (taking)
            {
                taking = false;
            }
        }
        
    }

    public static bool isTaking
    {
        get
        {
            return taking;
        }
    }
}
