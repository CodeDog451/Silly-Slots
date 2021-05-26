using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemController : MonoBehaviour
{
    private GameObject gemIdle;
    private GameObject gemBreak;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in gameObject.transform)
        {
            if (child.tag == "GemBreak")
            {
                gemBreak = child.gameObject;
            }
            else if (child.tag == "GemIdle")
            {
                gemIdle = child.gameObject;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        // this object was clicked - do something
        Debug.Log("Got a gem click");
        if(gemIdle != null)
        {
            gemIdle.SetActive(false);
        }
        if (gemBreak != null)
        {
            gemBreak.SetActive(true);
        }           


    }
}
