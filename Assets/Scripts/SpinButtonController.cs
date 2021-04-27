using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinButtonController : MonoBehaviour
{
    //public GameObject GameManagerObject;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void OnMouseDown()
    {
        // this object was clicked - do something
        //Debug.Log("Got a spin button click");
        if (gameManager != null)
        {
            gameManager.OnSpinButtonClick();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
