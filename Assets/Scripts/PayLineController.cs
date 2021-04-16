using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayLineController : MonoBehaviour
{
    public GameObject line;
    private SpriteRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = line.GetComponent<SpriteRenderer>();
        Debug.Log(lineRenderer);
    }

    public void ShowLine(bool show)
    {
        lineRenderer.enabled = show;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
