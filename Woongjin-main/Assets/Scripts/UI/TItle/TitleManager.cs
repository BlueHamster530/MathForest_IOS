using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField] FadeSceneChanger fadeoutSceneChanger;

    public bool isSetup;

    public void Setup()
    {
        isSetup = true;
    }

    private void Update()
    {
        if (!isSetup) return;

        if(Input.GetMouseButtonDown(0))
        {
            FadeGo();
        }
    }

    private void FadeGo()
    {
        fadeoutSceneChanger.gameObject.SetActive(true);

        if(PlayerPrefs.GetInt("isTutorialClear") == 0)
        {
            fadeoutSceneChanger.Setup("Tutorial");
            PlayerPrefs.SetInt("isTutorialClear", 1);
        }
        else
        {
            fadeoutSceneChanger.Setup("Menu");
        }
    }
}
