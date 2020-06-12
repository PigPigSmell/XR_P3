using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HoldHandle : MonoBehaviour
{
    public Transform handleOrigin;

    [HideInInspector]
    public static bool isHold = false;
    public static bool isRotate = false;

    private Move moveAction;
    private SteamVR_Behaviour_Boolean bb;
    private Vector3 originPosition;
    private Vector3 originRotation;
    private Vector3 uy;
    private Vector3 axis;
    private Quaternion oldRotation;
    private Quaternion newRotation;

    private void Awake()
    {
        bb = new SteamVR_Behaviour_Boolean();
        originPosition = handleOrigin.position;
        originRotation = handleOrigin.localRotation.eulerAngles;
        uy = new Vector3(0f, 1f, 0f);
        axis = new Vector3(1f, 0f, 0f);
        oldRotation = Quaternion.Euler(0f, 0f, 0f);
        moveAction = GameObject.Find("Game_Action").GetComponent<Move>();
    }

    private void Update()
    {
        if (isHold)
        {
            moveAction.enabled = false;
            newRotation = Quaternion.Euler(originRotation.x, originRotation.y, ComputeAngle());
            handleOrigin.transform.localRotation = newRotation;
            if (Mathf.Abs(newRotation.eulerAngles.y - oldRotation.eulerAngles.y) > 3f)
            {
                isRotate = true;
            }
            else
            {
                isRotate = false;
            }

            oldRotation = newRotation;

        }
        else
        {
            moveAction.enabled = true;
            isRotate = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (SteamVR_Input.GetState("HoldHandle", "Hold", bb.inputSource) && other.name == "HandleHead" && ControlCD.isPut)
        {
            handleOrigin.transform.localRotation = Quaternion.Euler(originRotation.x, originRotation.y, ComputeAngle());
            isHold = true;
        }
        if (!SteamVR_Input.GetState("HoldHandle", "Hold", bb.inputSource))
        {
            isHold = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isHold = false;
    }

    private float ComputeAngle()
    {
        float angle;
        Vector3 controllerPosition = transform.position;
        controllerPosition = controllerPosition - originPosition;
        controllerPosition.x = 0f;
        angle = -Vector3.SignedAngle(controllerPosition, uy, axis);

        return angle;
    }

}
