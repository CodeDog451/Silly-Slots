using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour
{
    private GameObject symbol;
    public string SymbolName
    {
        get;
        set;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        symbol = other.gameObject;
        SymbolName = symbol.name;
        //Debug.Log("got a Cell collision: " + SymbolName);
    }
}
