using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GetMagic : MonoBehaviour
{
    public int Magic;
    //private int Magic;

    private LineRenderer lineRenderer;
    private SteamVR_Behaviour_Boolean steamVR_Behaviour_Boolean = new SteamVR_Behaviour_Boolean();

    private bool touch;
    private int linelen = 2;

    // Start is called before the first frame update
    void Start()
    {
        Magic = 0;

        //添加LineRenderer组件
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        touch = false;
    }

    // Update is called once per frame
    void Update()
    {
        touch = SteamVR_Input.GetState("Maze", "ray", steamVR_Behaviour_Boolean.inputSource);

        this.GetComponent<LineRenderer>().enabled = touch;

        if (touch)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            //Debug.DrawLine(this.transform.position, this.transform.position + transform.forward * 2, Color.yellow);

            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.SetPosition(0, ray.origin);
            lineRenderer.SetPosition(1, ray.origin + ray.direction * linelen);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, linelen))
            {
                if (hit.collider.gameObject.tag == "magic")
                {
                    hit.collider.gameObject.SetActive(false);
                    Magic++;
                }
                /*else if(hit.collider.gameObject.tag == "obstacle")
                {
                    hit.collider.gameObject.SetActive(false);
                }*/
            }
        }
    }


    public int GetMagicNum()
    {
        if(Magic > 0)
        {
            return Magic--;
        }
        else
        {
            return Magic;
        }
    }
}
