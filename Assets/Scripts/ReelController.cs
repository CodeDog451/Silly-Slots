using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
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

    }


}
