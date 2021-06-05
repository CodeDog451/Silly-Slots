using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemController : MonoBehaviour
{
    private GameObject gemIdle;
    private GameObject gemBreak;
    public GameObject TextObject;
    private TextFloatingControl textController;
    // Start is called before the first frame update
    void Start()
    {
        if(TextObject != null)
        {
            textController = TextObject.GetComponent<TextFloatingControl>();
        }
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

    void SetWinAmount(int num)
    {
        if(textController != null)
        {
            textController.SetText(num);
        }
    }

    void OnMouseDown()
    {
        // this object was clicked - do something
        //Debug.Log("Got a gem click");
        var amount = (int)(100 * Random.Range(1, 10));
        SetWinAmount(amount);
        if (gemIdle != null)
        {
            gemIdle.SetActive(false);
        }
        if (gemBreak != null)
        {
            gemBreak.SetActive(true);
        }           


    }
}
