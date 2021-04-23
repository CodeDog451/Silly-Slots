using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CellController : MonoBehaviour
{
    private GameObject symbol;
    
    public Transform Frame
    {
        get;
        set;
        
    }
    public string SymbolName
    {
        get;
        set;
    }

    public int SymbolId
    {
        get;
        set;
    }
    public void ShowFrame(bool show)
    {
        var allChildren = symbol.transform.GetComponentsInChildren<Transform>();
        var frame = allChildren.Where(k => k.gameObject.name == "frame").FirstOrDefault();
        var spriteRender = frame.GetComponent<SpriteRenderer>();
        spriteRender.enabled = show;
    }

    public void ShowWinEffect(bool show)
    {
        var allChildren = symbol.transform.GetComponentsInChildren<Transform>();
        var item = allChildren.Where(k => k.gameObject.name == "WinEffect").FirstOrDefault();
        var spriteRender = item.GetComponent<SpriteRenderer>();
        spriteRender.enabled = show;
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
        var symbolController =  symbol.GetComponent<SymbolController>();
        //var allChildren = symbol.transform.GetComponentsInChildren<Transform>();
        //Frame = allChildren.Where(k => k.gameObject.name == "frame").FirstOrDefault();
        //var frame = symbol.transform.Find("iconBonus/frame");
        SymbolName = symbol.name;
        SymbolId = symbolController.SymbolId;
        //Debug.Log("got a Cell collision: " + SymbolName);
    }
}
