using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public class Symbol
    {
        public string SymbolName { get; set; }
        public int SymbolId { get; set; }
        //public Transform Frame { get; set; }
        public System.Action<bool> ShowFrame { get; set; }
    }
    public GameObject[] reels;
    private List<ReelController> reelControllers;
    private Symbol[,] cells;
    private bool pullingHandle = false;
    public float spinDuration;
    // Start is called before the first frame update
    void Start()
    {
        cells = new Symbol[3, 5];
        reelControllers = new List<ReelController>();
        //int reelIndex = 0;
        foreach (var reelGameObject in reels)
        {
            var reel = reelGameObject.GetComponent<ReelController>();
            reelControllers.Add(reel);
            //int rowIndex = 0;
            //foreach (var symbol in reel.symbols)
            //{
            //    cells[rowIndex, reelIndex] = symbol;
            //    rowIndex++;
            //}

            //reelIndex++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && !pullingHandle)
        { 
            pullingHandle = true;            
            foreach (var reel in reelControllers)
            {
                reel.SpinReel();
            }
            StartCoroutine(ReelSpinCountdownRoutine());
        }
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
        
        int[][] paylineDef = new int[][]
        {
            new int[] { 0, 0, 0, 0, 0 }, //payline 1
            new int[] { 1, 1, 1, 1, 1 }, //payline 2
            new int[] { 2, 2, 2, 2, 2 }, //payline 3
        };
        //paylineDef.Length

        int[] paylineIndexes = paylineDef[index];

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
                    ShowFrame = cell.ShowFrame
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

    private IEnumerator ReelSpinCountdownRoutine()
    {
        yield return new WaitForSeconds(spinDuration);
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
        for (int x = 0; x < 3; x++)
        {
            var payLine1 = GetPayLine(x);
            if (payLine1.Count() > 1)
            {
                foreach (var sym in payLine1)
                {
                    sym.ShowFrame(true);
                }
            }
        }
        //var payline1 = cells[0, 0] + "|" + cells[0, 1] + "|" + cells[0, 2] + "|" + cells[0, 3] + "|" + cells[0, 4];
        //Debug.Log(payLine1);

    }
}
