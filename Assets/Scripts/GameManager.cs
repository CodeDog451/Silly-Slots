using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public GameObject[] reels;
    private List<ReelController> reelControllers;
    private string[,] cells;
    private bool pullingHandle = false;
    public float spinDuration;
    // Start is called before the first frame update
    void Start()
    {
        cells = new string[3, 5];
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

    private IEnumerator ReelSpinCountdownRoutine()
    {
        yield return new WaitForSeconds(spinDuration);
        pullingHandle = false;
        int reelIndex = 0;
        foreach (var reel in reelControllers)
        {
            int rowIndex = 0;
            foreach(var symbol in reel.symbols)
            {
                cells[rowIndex, reelIndex] = symbol.SymbolName;
                rowIndex++;
            }
            reelIndex++;
        }
        Debug.Log(cells);

    }
}
