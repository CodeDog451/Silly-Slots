using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShineAnimatedLogoController : MonoBehaviour
{
    private Animator myAnimation;
    public float Delay;
    // Start is called before the first frame update
    void Start()
    {
        myAnimation = GetComponent<Animator>();
        StartCoroutine("AnimationDelay");
    }

    IEnumerator AnimationDelay()
    { 
        while (true)
        {
            myAnimation.enabled = false;
            yield return new WaitForSeconds(Delay);//not playing time
            myAnimation.enabled = true;
            myAnimation.Play("shineAnimatedLogoText", 0, 0);//start from first frame
            yield return new WaitForSeconds(2);//playing time
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
