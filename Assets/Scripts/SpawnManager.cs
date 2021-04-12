using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private ReelController reel;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        reel = transform.parent.GetComponent<ReelController>();
        rb = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        var symbol = other.GetComponent<SymbolController>();
        Debug.Log("got a Spawner collider with: " + other);
        if (symbol != null)
        {
            if (!symbol.hasSpawnNext)
            {
                //symbol.hasSpawnNext = true;
                //reel.SpawnRandomSymbol();
            }
        }
        

    }
}
