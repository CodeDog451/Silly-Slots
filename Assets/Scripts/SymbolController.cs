using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolController : MonoBehaviour
{
    public bool pullingHandle = true;
    public float spawnHeightTrigger;
    private bool hasSpawnNext = false;
    private float bottomLimit = 0;
    private ReelController reel;
    private float speed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        reel = transform.parent.GetComponent<ReelController>();

        pullingHandle = true;

    }

    // Update is called once per frame
    void Update()
    {
        
        if (pullingHandle)
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
            if (!hasSpawnNext && transform.position.y < spawnHeightTrigger)
            {
                hasSpawnNext = true;
                reel.SpawnRandomSymbol();
            }
            if (transform.position.y < bottomLimit)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("got a collision");
    }
}
