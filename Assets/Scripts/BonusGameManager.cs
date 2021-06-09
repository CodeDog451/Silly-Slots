using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BonusGameManager : MonoBehaviour
{
    private int won = 0;
    private int opened = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Win(int amount)
    {
        won = won + amount;
        opened++;
        if(opened >= 5)
        {
            SceneManager.LoadScene("MyGame");
        }
    }
}
