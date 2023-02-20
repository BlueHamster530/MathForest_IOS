using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SliderFillTwinkle : MonoBehaviour
{
    [SerializeField]
    float fTwinkleTime;

    float fCurrentTime;

    [SerializeField]
    Slider dangerslider;
    [SerializeField]
    Image DnamgeFill;

    public void ChangeSlideValue()
    {
        if (dangerslider.value >= 0.5f && dangerslider.value <= 0.7f)
        {
            fTwinkleTime = 1.5f;
        }

        if (dangerslider.value > 0.7f && dangerslider.value <= 0.9f)
        {
            fTwinkleTime = 3.0f;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (dangerslider.value >= 0.5f)
        {
            float fTime = 0;
            fCurrentTime += Time.deltaTime * fTwinkleTime;
            fTime = fCurrentTime;
            if (fTime >= 1.0f)
                fTime = 2.0f - fCurrentTime;
            DnamgeFill.color = Color.Lerp(new Color(1.0f, 1.0f, 1.0f, 0.2f), new Color(1.0f, 1.0f, 1.0f, 1.0f), fTime);
            if (fCurrentTime >= 2.0f)
            {
                fCurrentTime = 0;
            }
        }
        else
        {
            DnamgeFill.color = Color.Lerp(new Color(1.0f, 1.0f, 1.0f, 0.2f), new Color(1.0f, 1.0f, 1.0f, 1.0f), 1.0f);
        }
    }
}
