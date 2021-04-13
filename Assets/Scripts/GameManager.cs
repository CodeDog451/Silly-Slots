using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public class Symbol
    {
        public string SymbolName
        {
            get;
            set;
        }

        public string SymbolId
        {
            get;
            set;
        }
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
    private List<Symbol> GetPayLine()
    {
        List<Symbol> line = new List<Symbol>();
        int reelIndex = 0;        
        int[] paylineIndexes = new int[] { 1, 1, 1, 1, 1 }; //payline 1
        foreach (var reel in reelControllers)
        {
            int rowIndex = paylineIndexes[reelIndex];
            var cell = reel.CellControllers[rowIndex];
            if (!line.Any() || line.Exists(d => d.SymbolId == cell.SymbolId))
            {
                var sym = new Symbol()
                {
                    SymbolId = cell.SymbolId,
                    SymbolName = cell.SymbolName
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
        var payline1 = cells[0, 0] + "|" + cells[0, 1] + "|" + cells[0, 2] + "|" + cells[0, 3] + "|" + cells[0, 4];
        Debug.Log(payline1);

    }
}
