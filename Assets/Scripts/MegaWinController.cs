using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaWinController : MonoBehaviour
{
    // Start is called before the first frame update
    public Canvas TextCanvas;
    private bool _isVisable;
    public bool IsVisable
    {
        get { return _isVisable; }
    }
    public void SetVisible(bool isVisable)
    {
        _isVisable = isVisable;
        var renderers = GetComponentsInChildren<Renderer>();
        foreach (var item in renderers)
        {
            item.enabled = isVisable;
        }
        TextCanvas.gameObject.SetActive(isVisable);
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
