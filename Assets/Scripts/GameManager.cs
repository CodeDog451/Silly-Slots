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
    private Symbol[,] cells;
    private bool pullingHandle = false;
    public float spinDuration;
    public GameObject[] PayLines;
    private List<PayLineController> PayLineControllers;
    public TextMeshProUGUI scoreText;
    private int score;
    public TextMeshProUGUI wonText;
    public AudioClip spinSound;
    private AudioSource audio;

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
        new int[] { 0, 0, 0, 45, 200, 1200 },    //yeti id = 8
    };

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        score = 10000;
        scoreText.text = score.ToString();
        cells = new Symbol[3, 5];
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && !pullingHandle)
        { 
            pullingHandle = true;
            UpdateScore(-9);//one token per payline
            foreach (var reel in reelControllers)
            {
                reel.SpinReel();
            }
            foreach (var controller in PayLineControllers)
            {
                controller.ShowLine(false);
            }
            StartCoroutine(ReelSpinCountdownRoutine());
            //StartCoroutine(ReelSpinSoundCountdownRoutine());
            
        }
    }

    private void UpdateScore(int scoreToAdd)
    {
        if (scoreToAdd >= 0)
        {
            wonText.text = scoreToAdd.ToString();
        }
        else
        {
            //wonText.text = "0";
        }
        score += scoreToAdd;
        scoreText.text = score.ToString();
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
        
        
        //paylineDef.Length

        int[] paylineIndexes = PaylineDef[index];

        int reelIndex = 0;
        foreach (var reel in reelControllers)
        {
            int rowIndex = paylineIndexes[reelIndex];
            var cell = reel.CellControllers[rowIndex];
            if (!line.Any() || line.Exists(d => d.SymbolId == cell.SymbolId))
            {
                var sym = new Symbol()
                {
                    SymbolId = cell.SymbolId,
                    SymbolName = cell.SymbolName,
                    //Frame = cell.Frame,
                    ShowFrame = cell.ShowFrame,
                    ShowWinEffect = cell.ShowWinEffect

                };
                line.Add(sym);
            }
            else
            {
                break; //get out of the loop
            }
            reelIndex++;
        }  

        return line;
    }

    private IEnumerator ReelSpinSoundCountdownRoutine()
    {
        audio.volume = 1.0f;
        audio.Play();
        yield return new WaitForSeconds(spinDuration-1);
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
        int reelIndex = 0;
        foreach (var reel in reelControllers)
        {
            int rowIndex = 0;
            foreach(var cell in reel.CellControllers)
            {
                var sym = new Symbol()
                {
                    SymbolId = cell.SymbolId,
                    SymbolName = cell.SymbolName
                };
                cells[rowIndex, reelIndex] = sym;
                rowIndex++;
            }
            reelIndex++;
        }
        int won = 0;
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
        UpdateScore(won);
        
        //var payline1 = cells[0, 0] + "|" + cells[0, 1] + "|" + cells[0, 2] + "|" + cells[0, 3] + "|" + cells[0, 4];
        //Debug.Log(payLine1);

    }
}
