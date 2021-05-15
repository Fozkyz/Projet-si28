using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeAnimation : MonoBehaviour
{
    Image im;
    public float duration;
    public LeanTweenType in_type;
    // Start is called before the first frame update
    void Start()
    {
        im = GetComponent<Image>();
        LeanTween.alpha(im.rectTransform, 0f, duration).setEase(in_type);
    }

    // Update is called once per frame
    void Update()
    {
        /*Color tmp = im.color;
        if (tmp.a > 0)
        {
            tmp.a -= 1*(Time.deltaTime/5);
            im.color = tmp;
        }*/
    }
}
