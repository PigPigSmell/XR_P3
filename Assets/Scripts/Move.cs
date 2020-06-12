using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Move : MonoBehaviour
{
    public Rigidbody cameraSight;
    public Rigidbody cameraRig;
    public GameObject leftCon;
    public GameObject rightCon;
    public GameObject referenceObj;
    public float stepLong = 1f;
    public float stepTime = 1f;

    private bool leftUp = false;
    private bool rightUp = false;
    private bool isMoving = false;
    private Vector3 move;

    private void FixedUpdate()
    {
         Walk();
    }

    private void Walk()
    {
        if (!rightUp)
        {
            if (leftCon.transform.position.y > referenceObj.transform.position.y && !leftUp)
            {
                UpFoot(ref leftUp);
            }

            else if (leftCon.transform.position.y <= referenceObj.transform.position.y && leftUp)
            {
                DownFoot(ref leftUp);
            }
        }

        if (!leftUp)
        {
            if (rightCon.transform.position.y > referenceObj.transform.position.y && !rightUp)
            {
                UpFoot(ref rightUp);
            }
            else if (rightCon.transform.position.y <= referenceObj.transform.position.y && rightUp)
            {
                DownFoot(ref rightUp);
            }
        }
    }

    private void UpFoot(ref bool up)
    {
        up = true;
    }
    
    private void DownFoot(ref bool up)
    {
        up = false;
        if (!isMoving)
        {
            move = cameraSight.transform.forward;
            move.y = 0f;
            move = move.normalized;
            StartCoroutine(Step());
        }
    }

    private IEnumerator Step()
    {
        isMoving = true;
        float updateTime = 0.05f;
        float currTime = 0f;
        while (currTime < stepTime)
        {
            cameraRig.MovePosition(cameraRig.position + move * stepLong * (updateTime / stepTime));
            currTime += updateTime;

            yield return new WaitForSeconds(updateTime);
        }

        isMoving = false;

    }
}
