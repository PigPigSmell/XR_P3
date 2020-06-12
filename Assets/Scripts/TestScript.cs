using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public GameObject obj;

    private StartToSavePrincess script;

    private void Awake()
    {
        script = obj.GetComponent<StartToSavePrincess>();
    }

    private void Start()
    {
        //StartCoroutine(Loop());
        script.MyPrincessDontWorry();
    }

    private IEnumerator Loop()
    {
        while (true)
        {


            yield return new WaitForSeconds(0.5f);
        }
    }
}
