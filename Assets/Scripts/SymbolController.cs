using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolController : MonoBehaviour
{
    public int SymbolId;
    private bool _pullingHandle = true;
    private bool _stopping = false;
    public bool PullingHandle
    {
        get
        {
            return _pullingHandle;
        }
        set
        {
            _pullingHandle = value;
            if (!_pullingHandle)
            {
                _stopping = true;
            }
        }
    }

    
    

    private float spawnHeightTrigger = 9.8f;//6.4f;
    public bool hasSpawnNext = false;
    private float bottomLimit = 0;
    private ReelController reel;
    private float speed = 20f;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        reel = transform.parent.GetComponent<ReelController>();
        rb = transform.GetComponent<Rigidbody>();
        //PullingHandle = true;

    }

    // Update is called once per frame
    void Update()
    {

        if (PullingHandle)
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);


            if (!hasSpawnNext && transform.position.y < spawnHeightTrigger)
            {
                hasSpawnNext = true;
                reel.SpawnRandomSymbol();
            }
        }
        
            if (transform.position.y < bottomLimit)
            {
                Destroy(gameObject);
            }
        
        //else if(_stopping)
        //{
        //    rb.velocity = Vector3.zero;
        //    _stopping = false;
        //}
    }

    void OnTriggerEnter(Collider other)
    {
        if (!PullingHandle)
        {
            //Debug.Log("got a Symbol collider with: " + other);
        }
    }
    void OnCollisionExit(Collision other)
    {
        rb.velocity = Vector3.zero;
        //print("No longer in contact with " + other.transform.name);
    }
}
