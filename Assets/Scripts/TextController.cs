using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextController : MonoBehaviour
{
    public TextMeshProUGUI TextElement;
    private int animateValue;
    private int newValue;
    public void SetText(string text)
    {
        TextElement.text = text;
    }
    public void SetText(int num, bool animate = false, Action<string> callback = null)
    {
        if (animate)
        {            
            newValue = num;
            StartCoroutine(CountUpRoutine(callback));
        }
        else
        {
            animateValue = num;
            newValue = num;
            SetText(num.ToString());
        }

    }

    public void FinishAnimationEarly()
    {
        animateValue = newValue;
    }

    // Start is called before the first frame update
    void Start()
    {
        animateValue = 0;
        newValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator CountUpRoutine(Action<string> callback)
    {

        yield return new WaitForSeconds(0.1f);
        if (newValue > animateValue)
        {
            animateValue++;
            SetText(animateValue.ToString());            
            StartCoroutine(CountUpRoutine(callback));
        }
        else if (newValue < animateValue)
        {
            animateValue--;
            SetText(animateValue.ToString());
            StartCoroutine(CountUpRoutine(callback));
        }
        else
        {
            animateValue = newValue;
            SetText(animateValue.ToString());    
            if(callback != null)
            {
                callback(animateValue.ToString());
            }
        }
    }
}
