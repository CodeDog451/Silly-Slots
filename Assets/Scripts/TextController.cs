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
    //private AudioSource audioAnimate;
    public GameObject audioAnimateObject;
    private AudioSource audioAnimate;


    public void SetText(string text)
    {
        TextElement.text = text;
    }
    public void SetText(int num, bool animate = false, Action<string> callback = null)
    {
        if (animate)
        {            
            newValue = num;
            //Debug.Log(num);
            //Debug.Log(animate);
            //Debug.Log(gameObject.activeSelf);
           
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

    private void PlayAnimateSound()
    {
        if(audioAnimate != null)
        {
            audioAnimate.Play();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        animateValue = 0;
        newValue = 0;
        if (audioAnimateObject != null)
        {
            audioAnimate = audioAnimateObject.GetComponent<AudioSource>();
        }
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
            //Debug.Log(animateValue);
            SetText(animateValue.ToString());
            PlayAnimateSound();
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
            PlayAnimateSound();
            if (callback != null)
            {
                callback(animateValue.ToString());
            }
        }
    }
}
