using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMain : MonoBehaviour
{
    [SerializeField] GameObject dangerSlider;

    public void HideSlider()
    {
        dangerSlider.SetActive(false);
    }
}
