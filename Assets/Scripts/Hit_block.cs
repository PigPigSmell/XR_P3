using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Hit_block : MonoBehaviour
{
    public GameObject cam;
    public GameObject All;

    //public GameObject Player;

    //[Header("Maze")]
    //public GameObject environment;

    [Header("Standard")]
    //public GameObject standard_middle;
    public GameObject standard_low;
    
    [Header("Hand")]
    public GameObject leftHand;
    public GameObject rightHand;

    [Header("Feet")]
    public GameObject leftFoot;
    public GameObject rightFoot;

    [Header("MoveCube")]
    public GameObject move0;
    public GameObject move1;

    public bool start, isTrigger, move;
    private bool walk, jump;
    private int cnt, slide, walkStep, jumpStep, skipStep;
    private float startTime, baseLine, prevD;
    private SteamVR_Behaviour_Boolean steamVR_Behaviour_Boolean = new SteamVR_Behaviour_Boolean();
    private float x = 0, y = 0, z = 0;
    private Vector3 bias;

    Quaternion Q;

    AudioPlayer audioP;
    image_alpha img;

    // Start is called before the first frame update
    void Start()
    {
        audioP = GameObject.Find("AudioPlayer").GetComponent<AudioPlayer>();
        img = GameObject.Find("Image").GetComponent<image_alpha>();

        Q = standard_low.transform.rotation;

        walk = false;
        jump = false;
        slide = 0;
        cnt = 0;
        walkStep = 0;
        jumpStep = 0;
        skipStep = 0;
        move = false;
        start = false;

        //bias = All.transform.position - standard_low.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(standard_low.transform.position.y < -10)
        {
            img.close = 1;
            audioP.enabled = false;
        }

        isTrigger = SteamVR_Input.GetState("Maze", "hold", steamVR_Behaviour_Boolean.inputSource);

        //Move();

        //start = true;
        //prevD = Vector3.Distance(standard_low.transform.position, new Vector3(0, 0, 0));


        //Debug.Log(isTrigger);
        if (start)
        {
            Move();
            standard_low.transform.rotation = Q;
        }
        else
        {
            if (isTrigger)
            {
                //Debug.Log("innn");
                //All.transform.position = new Vector3(cam.transform.position.x + 2, cam.transform.position.y + 3f, cam.transform.position.z - 3);
                //All.transform.rotation = new Quaternion(0, cam.transform.rotation.y, 0, 0);
                standard_low.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z);
                //All.transform.position = standard_low.transform.position + bias;
                x = standard_low.transform.position.x - this.transform.position.x;
                //x = 0;
                y = standard_low.transform.position.y - this.transform.position.y;
                //y = 1;
                z = standard_low.transform.position.z - this.transform.position.z;
                //z = 0;
                //this.transform.position = new Vector3(standard_low.transform.position.x, standard_low.transform.position.y, standard_low.transform.position.z);
                //All.transform.position = new Vector3(cam.transform.position.x + 2, cam.transform.position.y + 4f, cam.transform.position.z - 6);
                start = true;
                prevD = Vector3.Distance(standard_low.transform.position, new Vector3(0, 0, 0));
            }
        }

        if (move){
            move0.transform.position += (move0.transform.forward * 1.25f + move0.transform.up) * Time.deltaTime * 2;
            standard_low.transform.position += (move0.transform.forward * 1.3f + move0.transform.up) * Time.deltaTime * 2;
        }
        if(move0.transform.position.y >= move1.transform.position.y)
        {
            //move0.transform.position = move1.transform.position;
            //standard_low.transform.position = move0.transform.position + new Vector3(0, 0.5f, 0);
            move = false;
        }
    }


    private void Move()
    {
        

        //if(leftFoot.transform.position.y > 0.8f && rightFoot.transform.position.y > 0.8f && slide == 0)
        if (slide == 0)
        {
            //Debug.Log(standard_low.transform.InverseTransformPoint());

            baseLine = standard_low.transform.position.y + 0.4f;

            if (!isTrigger)
            {
                /* walk */
                if (walkStep == 0)
                {
                    if (rightHand.transform.position.y < baseLine)
                    {
                        walkStep = 1;
                        walk = true;
                    }
                }
                else if (walkStep == 1)
                {
                    if (rightHand.transform.position.y > baseLine)
                    {
                        walkStep = 2;
                        walk = true;
                    }
                }

                if (walkStep == 2)
                {
                    standard_low.transform.position += new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z) * (0.6f);
                    audioP.WalkSound();
                    walkStep = 0;
                    walk = false;
                }
            }
            else
            {
                walkStep = 0;
                /* jump */
                if (!jump && rightHand.transform.position.y > baseLine + 0.3f)
                {
                    jump = true;
                    audioP.JumpSound();
                    standard_low.transform.position += new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z) * (0.3f);
                    standard_low.transform.position += new Vector3(0, 0.2f, 0);
                }

                if (jump && rightHand.transform.position.y < baseLine + 0.2f)
                {
                    jump = false;
                }
            }

            if (walk || jump)
            {
                startTime = Time.time;
            }
            else if (Time.time - startTime > 2)
            {
                audioP.stopSound();
                startTime = Time.time;
            }
        }
        else if (slide == 1)
        {
            // slide up
            standard_low.transform.position += new Vector3(cam.transform.forward.x * 0.3f, 0.4f, cam.transform.forward.z * 0.3f) * (Time.deltaTime) * 3;
        }
        else if (slide == -1)
        {
            // slide down
            standard_low.transform.position += new Vector3(cam.transform.forward.x * 0.3f, 0f, cam.transform.forward.z * 0.3f) * (Time.deltaTime) * 3;
        }

        this.transform.position = new Vector3(standard_low.transform.position.x - x, standard_low.transform.position.y - 1, standard_low.transform.position.z - z);

        if(!walk && !jump)
        {
            float currentD = Vector3.Distance(standard_low.transform.position, new Vector3(0, 0, 0));
            //Debug.Log(currentD);
            if (currentD < prevD)
            {
                skipStep++;
                if(skipStep > 10)
                {
                    skipStep = 0;
                    audioP.walkBack();
                }
            }
            prevD = currentD;
        }
    }
    
    /*private void OnTriggerExit(Collider other)
    {
        if(slide == 1 && other.gameObject.tag == "slideDown")
        {
            slide = 0;
        }
        else if(slide == -1 && other.gameObject.tag == "slideUp")
        {
            slide = 0;
        }
    }*/

    public void setSlide(int s)
    {
        slide = s;
    }

    public void setMove(bool mark)
    {
        move = mark;
    }
}
