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

    // Start is called before the first frame update
    void Start()
    {
        reel = transform.parent.GetComponent<ReelController>();

        pullingHandle = true;

    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.down * Time.deltaTime * speed);
        //if (pullingHandle)
        //{
        if (!hasSpawnNext && transform.position.y < spawnHeightTrigger)
        {
            hasSpawnNext = true;
            reel.SpawnRandomSymbol();
        }
        if (transform.position.y < bottomLimit)
        {
            Destroy(gameObject);
        }
        //}
    }
}
