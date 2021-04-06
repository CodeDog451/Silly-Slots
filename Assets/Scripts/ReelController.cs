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

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("SpawnRandomSymbol", startDelay, spawnInterval);
        SpawnRandomSymbol();
        StartCoroutine(ReelSpinCountdownRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && !pullingHandle)
        {
            var row1 = GameObject.FindGameObjectWithTag("Row1");
            var row1Collider = row1.GetComponent<BoxCollider>();
            //if (lastEnabled != row1Collider.enabled)
            //{
                row1Collider.enabled = !row1Collider.enabled;
            //}

            //pullingHandle = true;
            //var symbols = gameObject.GetComponentsInChildren<MoveDown>();
            //foreach (var sym in symbols)
            //{
            //    sym.pullingHandle = true;
            //}
            //StartCoroutine(ReelSpinCountdownRoutine());

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
        var symbols = gameObject.GetComponentsInChildren<MoveDown>();
        foreach(var sym in symbols)
        {
            sym.pullingHandle = false;
        }

    }


}
