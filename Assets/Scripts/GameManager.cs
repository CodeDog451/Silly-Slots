using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
public class GameManager : MonoBehaviour
{
    public class Symbol
    {
        public string SymbolName { get; set; }
        public int SymbolId { get; set; }
        //public Transform Frame { get; set; }
        public System.Action<bool> ShowFrame { get; set; }
        public System.Action<bool> ShowWinEffect { get; set; }
    }
    public GameObject[] reels;
    private List<ReelController> reelControllers;
    //private Symbol[,] cells;
    private bool pullingHandle = false;
    public float spinDuration;
    public GameObject[] PayLines;
    private List<PayLineController> PayLineControllers;
    public TextMeshProUGUI scoreText;
    private int score;
    private int freeSpins;
    public TextMeshProUGUI wonText;
    public TextMeshProUGUI spinsText;
    //public AudioClip spinSound;
    private AudioSource audio;
    public GameObject megaWin;
    private AudioSource megaWinAudio;
    
    public TextMeshProUGUI megaWinText;
    public GameObject coinDropSound;

    public GameObject winSound;
    private AudioSource winAudio;

    private AudioSource audioCoinDrop;

    private int megaWinLimit = 100;

    int won = 0;
    int animateWon = 0;

    private int[][] PaylineDef = new int[][]
        {
            new int[] { 0, 0, 0, 0, 0 }, //payline 1
            new int[] { 0, 0, 1, 0, 0 }, //payline 2
            new int[] { 0, 1, 2, 1, 0 }, //payline 3
            new int[] { 1, 0, 0, 0, 1 }, //payline 4
            new int[] { 1, 1, 1, 1, 1 }, //payline 5
            new int[] { 1, 2, 2, 2, 1 }, //payline 6
            new int[] { 2, 1, 0, 1, 2 }, //payline 7
            new int[] { 2, 2, 1, 2, 2 }, //payline 8
            new int[] { 2, 2, 2, 2, 2 }, //payline 9
        };

    public class SymbolIds
    {
        public static int Helmet = 1;
        public static int Pick = 2;
        public static int Climber = 3;
        public static int Boots = 4;
        public static int Hook = 5;
        public static int Tent = 6;
        public static int RockClimber = 7;
        public static int Yeti = 8;
        public static int FreeSpins = 9;
    }

    private int[][] PayTable = new int[][]
    {
        new int[] { 0, 0, 0, 0,  0,   0 },       //no symbol id = 0
        new int[] { 0, 0, 0, 15, 45,  200 },     //helmet id = 1
        new int[] { 0, 0, 0, 5,  20,  100 },     //pick id = 2
        new int[] { 0, 0, 0, 45, 200, 1200 },    //climber id = 3
        new int[] { 0, 0, 0, 15, 45,  200 },     //boots id = 4
        new int[] { 0, 0, 0, 10, 30,  150 },     //hook id = 5
        new int[] { 0, 0, 0, 5,  20,  100 },     //tent id = 6
        new int[] { 0, 0, 0, 10, 30,  150 },     //rock climber id = 7
        new int[] { 0, 0, 0, 45, 200, 1200 },    //yeti id = 8 //wild card
        new int[] { 0, 0, 0, 5,  10,  20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120 },     //free spins id = 9
    };

    public void OnSpinButtonClick()
    {
        SpinIfReady();
    }

    public void OnCloseButtonClick()
    {
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        score = PlayerPrefs.GetInt("score", score);

        winAudio = winSound.GetComponent<AudioSource>();

        audioCoinDrop = coinDropSound.GetComponent<AudioSource>();
        audio = GetComponent<AudioSource>();
        megaWinAudio = megaWin.GetComponent<AudioSource>();
        score = PlayerPrefs.GetInt("score", score);
        if (!(score > 0))
        {
            score = 10000;
        }
        scoreText.text = score.ToString();

        freeSpins = 0;
        spinsText.text = freeSpins.ToString();

        reelControllers = new List<ReelController>();        
        foreach (var reelGameObject in reels)
        {
            var reel = reelGameObject.GetComponent<ReelController>();
            reelControllers.Add(reel);            
        }
        PayLineControllers = new List<PayLineController>();
        foreach(var payline in PayLines)
        {
            var controller = payline.GetComponent<PayLineController>();
            PayLineControllers.Add(controller);
        }
    }

    private void SpinIfReady()
    {
        if (!pullingHandle)
        {
            megaWin.SetActive(false);
            megaWinAudio.Stop();
            coinDropSound.SetActive(false);
            pullingHandle = true;
            if (freeSpins > 0)
            {
                UpdateSpins(-1); 
            }
            else
            {
                UpdateScore(-9);//one token per payline
            }
            foreach (var reel in reelControllers)
            {
                reel.SpinReel();
            }
            foreach (var controller in PayLineControllers)
            {
                controller.ShowLine(false);
            }
            StartCoroutine(ReelSpinCountdownRoutine());
            StartCoroutine(ReelSpinSoundCountdownRoutine());

        }
    }

    // Update is called once per frame
    void Update()
    {
        //if(animateWon < won)
        //{
        //    animateWon++;
        //    megaWinText.text = animateWon.ToString();
        //};
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter))
        {
            SpinIfReady();  
        }
    }

    private void UpdateScore(int scoreToAdd)
    {
        if(scoreToAdd == 0)
        {
            wonText.text = scoreToAdd.ToString();
        }
        else if (scoreToAdd > 0)
        {
            winAudio.Play();
            //wonText.text = scoreToAdd.ToString();
            //megaWinText.text = scoreToAdd.ToString();
        }
        else
        {
            //wonText.text = "0";
        }
        score += scoreToAdd;
        PlayerPrefs.SetInt("score", score);
        scoreText.text = score.ToString();
    }

    private void UpdateSpins(int spinsToAdd)
    {
        freeSpins += spinsToAdd;
        spinsText.text = freeSpins.ToString();
    }

    /// <summary>
    /// Find the sequence of matching symbols from left to right
    /// Return the list of matching symbols and the list length is the number of matching.
    /// No mismatched symbols should be in this returned list
    /// </summary>
    /// <returns></returns>
    private List<Symbol> GetPayLine(int index)
    {        

        List<Symbol> line = new List<Symbol>();       

        int[] paylineIndexes = PaylineDef[index];

        int reelIndex = 0;
        foreach (var reel in reelControllers)
        {
            int rowIndex = paylineIndexes[reelIndex];
            var cell = reel.CellControllers[rowIndex];
            
            if (!line.Any() || line.Exists(d => d.SymbolId == cell.SymbolId  || cell.SymbolId == SymbolIds.Yeti))
            {
                var sym = new Symbol()
                {
                    SymbolId = cell.SymbolId,
                    SymbolName = cell.SymbolName,
                    //Frame = cell.Frame,
                    ShowFrame = cell.ShowFrame,
                    ShowWinEffect = cell.ShowWinEffect

                };
                if (cell.SymbolId == SymbolIds.Yeti)
                {
                    line.Add(sym);
                }
                else
                {
                    line.Insert(0, sym);
                }
            }
            else
            {
                break; //get out of the loop
            }
            reelIndex++;
        }  

        return line;
    }

    private List<Symbol> GetScatterLine(int symbolId)
    {
        List<Symbol> line = new List<Symbol>();        
        foreach (var reel in reelControllers)
        {
            
            foreach (var cell in reel.CellControllers)
            {
                
                if (cell.SymbolId == symbolId)
                {
                    line.Add(new Symbol()
                    {
                        SymbolId = cell.SymbolId,
                        SymbolName = cell.SymbolName,
                        ShowFrame = cell.ShowFrame,
                        ShowWinEffect = cell.ShowWinEffect

                    });
                }
            }   
        }

        return line;
    }

    private IEnumerator WonCountUpRoutine()
    {
        
        yield return new WaitForSeconds(0.1f);
        if (animateWon < won && coinDropSound.activeSelf)
        {
            audioCoinDrop.Play();
            animateWon++;
            megaWinText.text = animateWon.ToString();
            wonText.text = animateWon.ToString();
            StartCoroutine(WonCountUpRoutine());
        }
        else
        {
            wonText.text = won.ToString();
            StartCoroutine(WonCountUpFinishRoutine());
        }
    }

    private IEnumerator WonCountUpFinishRoutine()
    {
        yield return new WaitForSeconds(1.0f);
        //UpdateScore(won);
        megaWinAudio.Stop();
        megaWin.SetActive(false);
    }
    private IEnumerator ReelSpinSoundCountdownRoutine()
    {
        audio.volume = 1.0f;
        audio.Play();
        yield return new WaitForSeconds(spinDuration);
        audio.volume = 0.0f;
        

    }
    private IEnumerator ReelSpinCountdownRoutine()
    {
        //audio.volume = 1.0f;
        //audio.Play();
        yield return new WaitForSeconds(spinDuration);
        //audio.volume = 0.0f;
        //audio.Stop();
        pullingHandle = false;
        
        won = 0;
        animateWon = 0;
        for (int x = 0; x < PaylineDef.Length; x++)
        {
            //PayTable
            var payLine1 = GetPayLine(x);
            if (payLine1.Count() > 2)
            {
                
                var symbolId = payLine1.First().SymbolId;
                var payAmount = PayTable[symbolId][payLine1.Count()];
                

                PayLineControllers[x].ShowLine(true);
                foreach (var sym in payLine1)
                {
                    sym.ShowFrame(true);
                    sym.ShowWinEffect(true);
                }
                won += payAmount;
                //UpdateScore(payAmount);
            }
        }
        var freeSpins = GetScatterLine(9);
        if(freeSpins.Count > 2)
        {
            foreach (var sym in freeSpins)
            {
                sym.ShowFrame(true);
                sym.ShowWinEffect(true);
            }
            //kmp
            var awardSpins = PayTable[9][freeSpins.Count()];
            UpdateSpins(awardSpins);
        }
        if (won > 0)
        {
            animateWon++;
            megaWinText.text = animateWon.ToString();
            if (won >= megaWinLimit)
            {
                megaWin.SetActive(true);
                megaWinAudio.Play();
            }
            coinDropSound.SetActive(true);

            StartCoroutine(WonCountUpRoutine());
        }
        UpdateScore(won);    

    }
}
