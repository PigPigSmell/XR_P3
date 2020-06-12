using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSoul : MonoBehaviour
{
    public GameObject rightHand;
    public GameObject leftHand;
    public GameObject soul;
    
    // Start is called before the first frame update
    void Start()
    {
        soul.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(rightHand.transform.position, leftHand.transform.position) < 0.2f)
        {
            soul.transform.position = (rightHand.transform.position + leftHand.transform.position) * 0.5f;
            soul.SetActive(true);
        }
        else
        {
            soul.SetActive(false);
        }
    }
}
