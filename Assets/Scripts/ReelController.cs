using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReelController : MonoBehaviour
{
    
    public GameObject[] symbolsPrefabs;
    private float startDelay = 2;
    private float spawnInterval = 0.1f;
    private bool pullingHandle = false;
    private bool lastEnabled = false;
    private GameObject row1;
    private List<BoxCollider> rowColliders = new List<BoxCollider>();
    public List<CellController> CellControllers;
    public GameObject[] cells;
    public float spinDuration;
    public GameObject[] reelStops;
    private AudioSource audio;
    //private AudioSource audio;


    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        CellControllers = new List<CellController>();
        //row1 = GameObject.FindGameObjectWithTag("Row1");
        //row1 = reelStop;
        foreach(var reelStop in reelStops)
        {
            rowColliders.Add(reelStop.GetComponent<BoxCollider>());
        }
        //if (row1 != null)
        //{
            
        //}
        //InvokeRepeating("SpawnRandomSymbol", startDelay, spawnInterval);
        SpawnRandomSymbol();
        StartCoroutine(ReelSpinCountdownRoutine(0.5f));
    }

    public void ReelRowCollider(bool state)
    {
        foreach (var rowCollider in rowColliders)
        {
            rowCollider.enabled = state;
        }
    }
    public void SpinReel()
    {
        ReelRowCollider(false);
        pullingHandle = true;
        var symbols = gameObject.GetComponentsInChildren<SymbolController>();
        foreach (var sym in symbols)
        {
            sym.PullingHandle = true;
        }
        StartCoroutine(ReelSpinCountdownRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.Space) && !pullingHandle)
        //{
        //    ReelRowCollider(false);          

        //    pullingHandle = true;
        //    var symbols = gameObject.GetComponentsInChildren<SymbolController>();
        //    foreach (var sym in symbols)
        //    {
        //        sym.PullingHandle = true;
        //    }
        //    StartCoroutine(ReelSpinCountdownRoutine());

        //}
    }

    public void SpawnRandomSymbol()
    {
        int index = Random.Range(0, symbolsPrefabs.Length);
        Vector3 spawnPos = new Vector3(0, 11.0f, 0);//8.0f
        GameObject child = Instantiate(symbolsPrefabs[index], transform.position + spawnPos, symbolsPrefabs[index].transform.rotation);
        child.transform.SetParent(gameObject.transform);
    }

    private IEnumerator ReelSpinCountdownRoutine(float? init = null)
    {
        //audio.volume = 0.5f;
        // audio.Play();
        float spinTime = spinDuration;
        if(init != null)
        {
            spinTime = init.Value;
        }
        yield return new WaitForSeconds(spinTime);
        audio.Play();
        //audio.volume = 0.0f;
        pullingHandle = false;
        ReelRowCollider(true);
        //rowCollider.enabled = true;
        var symbols = gameObject.GetComponentsInChildren<SymbolController>();
        foreach(var sym in symbols)
        {
            sym.PullingHandle = false;
        }
        StartCoroutine(ReelSpinStopRoutine());
        //int index = 0;
        //foreach (var cell in cells)
        //{
        //    //var allChildren = cell.transform.GetComponentsInChildren<Transform>();
        //    //var frame = allChildren.Where(k => k.gameObject.name == "frame").FirstOrDefault();
        //    var symbol1 = cell.GetComponent<CellController>();
        //    symbol1.ShowFrame(false);
        //    var symbolName = symbol1.SymbolName;
        //    //Debug.Log("cell: " + index + " : " + symbolName);
        //    index++;
        //}
    }



    private IEnumerator ReelSpinStopRoutine()
    {
        yield return new WaitForSeconds(1);

        CellControllers.Clear();
        int index = 0;
        foreach (var cell in cells)
        {
            //var allChildren = cell.transform.GetComponentsInChildren<Transform>();
            //var frame = allChildren.Where(k => k.gameObject.name == "frame").FirstOrDefault();
            var symbol1 = cell.GetComponent<CellController>();
            CellControllers.Add(symbol1);
            //symbol1.ShowFrame(false);
            //symbol1.ShowWinEffect(false);
            var symbolName = symbol1.SymbolName;
            //Debug.Log("cell: " + index + " : " + symbolName);
            index++;
        }


    }


}
