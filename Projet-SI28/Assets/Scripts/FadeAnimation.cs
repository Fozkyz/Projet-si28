using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeAnimation : MonoBehaviour
{
    Image im;
    public float duration;
    public LeanTweenType in_type;

	private void OnEnable()
	{
        im = GetComponent<Image>();
        im.color = Color.black;
        LeanTween.alpha(im.rectTransform, 0f, duration).setEase(in_type);
    }

}
