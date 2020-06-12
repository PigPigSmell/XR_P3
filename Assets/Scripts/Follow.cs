using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public float distance;

    private Vector3 d;

    private void Awake()
    {

        d = new Vector3(-distance, -transform.parent.position.y, 0f);
    }

    private void FixedUpdate()
    {
        transform.localPosition = d;
    }
}
