using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BonusGameManager : MonoBehaviour
{
    private int won = 0;
    private int opened = 0;
    private int score;
    // Start is called before the first frame update
    void Start()
    {
        score = PlayerPrefs.GetInt("score", 0);
        Debug.Log("Score start bonus game: " + score.ToString());
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
            score = score + won;
            Debug.Log("Score end bonus game: " + score.ToString());
            PlayerPrefs.SetInt("score", score);
            SceneManager.LoadScene("MyGame");
        }
    }
}
