using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource audioPlayer;
    public AudioSource audioPlayer_background;
    public AudioSource[] mixAudioPlayer = new AudioSource[3];

    public AudioClip[] spring_walk = new AudioClip[7];
    public AudioClip[] spring_stop2 = new AudioClip[7];
    public AudioClip[] spring_jump = new AudioClip[5];

    public AudioClip[] winter_walk = new AudioClip[7];
    public AudioClip[] winter_stop2 = new AudioClip[6];
    public AudioClip[] winter_jump = new AudioClip[7];

    public AudioClip[] slides = new AudioClip[4];
    public AudioClip[] background = new AudioClip[2];
    public AudioClip[] spring_special = new AudioClip[2];
    public AudioClip[] winter_special = new AudioClip[2];
    public AudioClip[] noise = new AudioClip[11];
    public AudioClip move;

    private int idx, step;

    // Start is called before the first frame update
    void Start()
    {
        idx = 0;
        step = 0;
        audioPlayer_background.clip = background[step];
        audioPlayer_background.Play();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown("down"))
        {
            step++;
            step %= 2;
        }*/
    }

    public void WalkSound()
    {
        audioPlayer.Stop();
        idx = (int)(Random.Range(0, 7));
        if(step == 0)
        {
            audioPlayer.clip = spring_walk[idx];
        }
        if (step == 1)
        {
            audioPlayer.clip = winter_walk[idx];
        }
        audioPlayer.Play();
    }

    public void JumpSound()
    {
        audioPlayer.Stop();
        idx = (int)(Random.Range(0, 5));
        if(step == 0)
        {
            audioPlayer.clip = spring_jump[idx];
        }
        if (step == 1)
        {
            audioPlayer.clip = winter_jump[idx];
        }
        audioPlayer.Play();
        
    }

    public void stopSound()
    {
        audioPlayer.Stop();
        idx = (int)(Random.Range(0, 7));
        if(step == 0)
        {
            audioPlayer.clip = spring_stop2[idx];
        }
        if(step == 1)
        {
            audioPlayer.clip = winter_stop2[idx];
        }
        audioPlayer.Play();
        
    }

    public void slideUpSound()
    {
        audioPlayer.Stop();
        if(step == 0)
        {
            audioPlayer.clip = slides[0];
        }
        if(step == 1)
        {
            audioPlayer.clip = slides[1];
        }
        audioPlayer.Play();
        
    }

    public void slideDownSound()
    {
        audioPlayer.Stop();
        if(step == 0)
        {
            audioPlayer.clip = slides[2];
        }
        if(step == 1)
        {
            audioPlayer.clip = slides[3];
        }
        audioPlayer.Play();
    }

    public void moveMaze()
    {
        audioPlayer.Stop();
        audioPlayer.clip = move;
        audioPlayer.Play();
    }

    public void SpecialSound(int num)
    {
        mixAudioPlayer[0].Stop();
        if (num == 0) // beautiful
        {
            idx = (int)(Random.Range(0, 5));
            if (step == 0)
            {
                mixAudioPlayer[0].clip = spring_special[idx];
            }
            if(step == 1)
            {
                Debug.Log("beautiful");
                mixAudioPlayer[0].clip = winter_special[idx];
            }
        }
        if (num == 1) // noise
        {
            Debug.Log("noise");
            idx = (int)(Random.Range(0, 9));
            mixAudioPlayer[0].clip = noise[idx];
        }
        mixAudioPlayer[0].Play();
    }

    public void walkBack()
    {
        //audioPlayer.Stop();
        //audioPlayer.clip = noise[0];
        //audioPlayer.Play();
    }

    public void StepPlus()
    {
        step++;
        step %= 2;
        audioPlayer_background.clip = background[step];
        audioPlayer_background.Play();
    }
}
