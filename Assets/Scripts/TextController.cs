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
    public void SetText(int num, bool animate = false)
    {
        if (animate)
        {
            newValue = num;
            StartCoroutine(CountUpRoutine());
        }
        else
        {
            animateValue = num;
            newValue = num;
            SetText(num.ToString());
        }
        
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

    private IEnumerator CountUpRoutine()
    {

        yield return new WaitForSeconds(0.1f);
        if (newValue > animateValue)
        {
            animateValue++;
            SetText(animateValue.ToString());            
            StartCoroutine(CountUpRoutine());
        }
        else if (newValue < animateValue)
        {
            animateValue--;
            SetText(animateValue.ToString());
            StartCoroutine(CountUpRoutine());
        }
        else
        {
            animateValue = newValue;
            SetText(animateValue.ToString());            
        }
    }
}
