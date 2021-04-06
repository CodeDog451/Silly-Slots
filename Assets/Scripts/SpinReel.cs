using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinReel : MonoBehaviour
{
    private bool pullingHandle = false;
    public float spinSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && !pullingHandle)
        {
            pullingHandle = true;
            StartCoroutine(ReelSpinCountdownRoutine());
        }
        if (pullingHandle)
        {

            transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
        }
    }

    private IEnumerator ReelSpinCountdownRoutine()
    {
        yield return new WaitForSeconds(5);
        pullingHandle = false;
        
    }
}
