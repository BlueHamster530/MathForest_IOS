using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WJ_TimerImageSwap : MonoBehaviour
{
    [SerializeField]
    WJ_Sample sample;

    [SerializeField]
    Sprite[] ChangeImage;
    float fChangeTime;
    int nImageIndex;

    Image image;

    [SerializeField] Slider TimerSlider;
    
    float fQusetionTime;

    private void Start()
    {
        fQusetionTime = 20.0f;
        fChangeTime = 0.3f;
        nImageIndex = 0;
        image = GetComponent<Image>();
        image.sprite = ChangeImage[nImageIndex];
    }
    private void OnEnable()
    {
        fQusetionTime = 20.0f;
        TimerSlider.value = fQusetionTime / 20.0f;
    }
    private void Update()
    {
        fChangeTime -= Time.unscaledDeltaTime;
        if (fChangeTime <= 0.0f)
        {
            fChangeTime = 0.3f;
            if (nImageIndex == 0)
                nImageIndex = 1;
            else if (nImageIndex == 1)
                nImageIndex = 0;
            image.sprite = ChangeImage[nImageIndex];
        }
        fQusetionTime -= Time.unscaledDeltaTime;
        TimerSlider.value = fQusetionTime / 20.0f;
        if (fQusetionTime <= 0)
        {
            sample.DoDN_WongAnswer();
        }
    }
}
