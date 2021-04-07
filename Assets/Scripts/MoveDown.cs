using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    public bool pullingHandle = true;
    private float speed = 10f;
    public ReelController reel;
    private bool hasSpawnNext = false;
    public float spawnHeightTrigger;
    private float bottomLimit = 0;
    // Start is called before the first frame update
    void Start()
    {
        //reel = transform.parent.GetComponent<ReelController>();
        //StartCoroutine(ReelSpinCountdownRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
        //if (pullingHandle)
        //{
            //transform.Translate(Vector3.down * Time.deltaTime * speed);

            if (!hasSpawnNext && transform.position.y < spawnHeightTrigger)
            {
                hasSpawnNext = true;
                //reel.SpawnRandomSymbol();
            }
            if (transform.position.y < bottomLimit)
            {
                Destroy(gameObject);
            }
        //}
        //else if(transform.position.y > 1.94f && transform.position.y < 3.0f)
        //{
        //    //need to make an invisable collider instead
        //    transform.localPosition = new Vector3(transform.localPosition.x, 1.94f, transform.localPosition.z);
        //}
        
    }
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("got a collision");
    }



}
