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
    private BoxCollider row1Collider;
    private List<GameObject> symbols;
    public GameObject[] cells;
    

    // Start is called before the first frame update
    void Start()
    {
        symbols = new List<GameObject>();
        row1 = GameObject.FindGameObjectWithTag("Row1");
        row1Collider = row1.GetComponent<BoxCollider>();
        //InvokeRepeating("SpawnRandomSymbol", startDelay, spawnInterval);
        SpawnRandomSymbol();
        StartCoroutine(ReelSpinCountdownRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && !pullingHandle)
        { 
            row1Collider.enabled = false;            

            pullingHandle = true;
            var symbols = gameObject.GetComponentsInChildren<SymbolController>();
            foreach (var sym in symbols)
            {
                sym.pullingHandle = true;
            }
            StartCoroutine(ReelSpinCountdownRoutine());

        }
    }

    public void SpawnRandomSymbol()
    {
        int index = Random.Range(0, symbolsPrefabs.Length);
        Vector3 spawnPos = new Vector3(0, 7.5f, 0);
        GameObject child = Instantiate(symbolsPrefabs[index], transform.position + spawnPos, symbolsPrefabs[index].transform.rotation);
        child.transform.SetParent(gameObject.transform);
    }

    private IEnumerator ReelSpinCountdownRoutine()
    {
        yield return new WaitForSeconds(5);
        pullingHandle = false;
        row1Collider.enabled = true;
        var symbols = gameObject.GetComponentsInChildren<SymbolController>();
        foreach(var sym in symbols)
        {
            sym.pullingHandle = false;
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
       
       
        int index = 0;
        foreach (var cell in cells)
        {
            //var allChildren = cell.transform.GetComponentsInChildren<Transform>();
            //var frame = allChildren.Where(k => k.gameObject.name == "frame").FirstOrDefault();
            var symbol1 = cell.GetComponent<CellController>();
            symbol1.ShowFrame(false);
            var symbolName = symbol1.SymbolName;
            //Debug.Log("cell: " + index + " : " + symbolName);
            index++;
        }


    }


}
