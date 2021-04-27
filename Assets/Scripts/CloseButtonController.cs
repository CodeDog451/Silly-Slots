using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButtonController : MonoBehaviour
{
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void OnMouseDown()
    {
        // this object was clicked - do something
        //Debug.Log("Got a close button click");
        if (gameManager != null)
        {
            gameManager.OnCloseButtonClick();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
