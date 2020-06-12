using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSth : MonoBehaviour
{
    AudioPlayer audioP;
    Hit_block hb;
    GetMagic gm;


    private int slide = 0, score = 0;
    private float time = -1;

    // Start is called before the first frame update
    void Start()
    {
        audioP = GameObject.Find("AudioPlayer").GetComponent<AudioPlayer>();
        //hb = GameObject.Find("temp").GetComponent<Hit_block>();
        hb = GameObject.Find("[CameraRig]").GetComponent<Hit_block>();
        //gm = GameObject.Find("right").GetComponent<GetMagic>();
        gm = GameObject.Find("Controller (right)").GetComponent<GetMagic>();
        slide = 0;
        score = 0;
        time = -1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "move")
        {
            audioP.moveMaze();
            audioP.StepPlus();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "move")
        {
            if (time < 0)
            {
                time = Time.time;
            }
            else if(Time.time - time > 2)
            {
                hb.setMove(true);
                audioP.moveMaze();
            }
        }
        
        if (slide == 0)
        {
            if (other.tag == "slideUp")
            {
                // slide up
                hb.setSlide(1);
                audioP.slideUpSound();
                slide = 1;
            }
            else if (other.tag == "slideDown")
            {
                // slide down
                hb.setSlide(-1);
                audioP.slideDownSound();
                slide = -1;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.tag == "slideUp" && slide == -1) || (other.tag == "slideDown" && slide == 1))
        {
            // slide up
            hb.setSlide(0);
            slide = 0;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "obstacle")
        {
            int num = gm.GetMagicNum();
            if (num > 0)
            {
                audioP.SpecialSound(0);
                score++;
            }
            else
            {
                audioP.SpecialSound(1);
            }
            collision.gameObject.SetActive(false);
        }
    }

    public int SentScore()
    {
        return score;
    }
}
