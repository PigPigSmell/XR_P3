using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ControlCD : MonoBehaviour
{
    public GameObject hand;
    public GameObject platform;
    public GameObject phonograph;
    public GameObject particle;
    public Vector3 locationOnHand;
    public Vector3 rotationOnHand;
    public Vector3 locationOnPlatform;
    public Vector3 rotationOnPlatform;

    [HideInInspector]
    public static bool isPut = false;

    private SteamVR_Behaviour_Boolean bb;
    private HoldHandle hh;
    
    private void Awake()
    {
        bb = new SteamVR_Behaviour_Boolean();
        hh = hand.GetComponent<HoldHandle>();
    }

    private void Start()
    {
        transform.SetParent(hand.transform);
        transform.localPosition = locationOnHand;
        transform.localRotation = Quaternion.Euler(rotationOnHand);
        hh.enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (SteamVR_Input.GetState("HoldHandle", "Hold", bb.inputSource) && !isPut)
        {
            transform.SetParent(phonograph.transform);
            transform.localPosition = locationOnPlatform;
            transform.localRotation = Quaternion.Euler(rotationOnPlatform);
            particle.SetActive(false);
            isPut = true;
        }
    }
}
