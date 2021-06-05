using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextFloatingControl : MonoBehaviour
{
    public TextMeshProUGUI TextElement;
    public float speed;
    bool animateUp = false;
    float moved = 0;
    // Start is called before the first frame update
    public void SetText(string text)
    {
        
        TextElement.text = text;
        animateUp = true;
    }
    public void SetText(int num)
    {
        TextElement.gameObject.SetActive(true);
        SetText("+" + num.ToString()); 
    }
    void Start()
    {
        TextElement.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (animateUp)
        {
            
            float move = Time.deltaTime * speed;
            moved = moved + move;
            if (moved > 1)
            {
                if (TextElement.faceColor.a > 0)
                {
                    var fade = Time.deltaTime * 500;
                    var alpha = TextElement.faceColor.a - fade;
                    if (alpha < 0) alpha = 0;
                    TextElement.faceColor = new Color32(TextElement.faceColor.r, TextElement.faceColor.g, TextElement.faceColor.b, Convert.ToByte(alpha));
                }
            }
            if (moved < 5)
            {
                transform.Translate(Vector3.up * Time.deltaTime * speed);
            }
        }
    }
}
