using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class computer_walk : MonoBehaviour
{
    public GameObject left;
    public GameObject right;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("up"))
        {
            left.transform.position += new Vector3(0, 1f, 0);
            right.transform.position += new Vector3(0, 1f, 0);
        }
        else if (Input.GetKeyDown("down"))
        {
            right.transform.position -= new Vector3(0, 0.5f, 0);
        }
        else if (Input.GetKeyDown("left"))
        {
            left.transform.position += new Vector3(0, 0.5f, 0);
        }
        else if (Input.GetKeyDown("right"))
        {
            right.transform.position += new Vector3(0, 0.5f, 0);
        }

        if (Input.GetKeyUp("up"))
        {
            left.transform.position -= new Vector3(0, 1f, 0);
            right.transform.position -= new Vector3(0, 1f, 0);
        }
        else if (Input.GetKeyUp("down"))
        {
            right.transform.position += new Vector3(0, 0.5f, 0);
        }
        else if (Input.GetKeyUp("left"))
        {
            left.transform.position -= new Vector3(0, 0.5f, 0);
        }
        else if (Input.GetKeyUp("right"))
        {
            right.transform.position -= new Vector3(0, 0.5f, 0);
        }
    }
}
