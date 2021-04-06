using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullHandle : MonoBehaviour
{
    public float spinSpeed;
    // Start is called before the first frame update
    private bool pullingHandle = false;
    private float rotation = 0;
    private float direction = 1;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Space) && !pullingHandle)
        {
            pullingHandle = true;
            
            
        }
        if (pullingHandle)
        {
            rotation = rotation + spinSpeed * Time.deltaTime * direction;
            transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime * direction);
            
            //Debug.Log("rotation:" + rotation.ToString());
            
            if (rotation > 90)
            {
                
                direction = -1;
            }
            else if(rotation < 0)
            {
                pullingHandle = false;
                direction = 1;
            }
        }        
    }    
}
