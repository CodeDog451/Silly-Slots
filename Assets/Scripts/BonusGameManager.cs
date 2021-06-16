using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BonusGameManager : MonoBehaviour
{
    private int won = 0;
    private int opened = 0;
    private int score;
    List<int> bonusLines = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        score = PlayerPrefs.GetInt("score", 0);
        Debug.Log("Score start bonus game: " + score.ToString());
        string bonusLinesJson = PlayerPrefs.GetString("bonusLines");
        bonusLines = JsonConvert.DeserializeObject<List<int>>(bonusLinesJson);
        foreach (var item in bonusLines)
        {
            Debug.Log("bonusLines item: " + item.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Win(int amount)
    {
        won = won + amount;
        opened++;
        var bonusGames = bonusLines.FirstOrDefault();
        if (opened >= bonusGames)
        {
            if(bonusLines.Count() > 0)
            {
                bonusLines.RemoveAt(0);
            }
            score = score + won;
            Debug.Log("Score end bonus game: " + score.ToString());
            PlayerPrefs.SetInt("score", score);
            if (bonusLines.Count() > 0)
            {
                //reload bonus game
                string bonusLinesJson = JsonConvert.SerializeObject(bonusLines);
                PlayerPrefs.SetString("bonusLines", bonusLinesJson);
                SceneManager.LoadScene("BonusGems");
            }
            else
            {
                SceneManager.LoadScene("MyGame");
            }
        }
    }
}
