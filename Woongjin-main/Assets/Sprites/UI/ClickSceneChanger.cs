using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ClickSceneChanger : MonoBehaviour
{
    [SerializeField] float setTime;   
    [SerializeField] string sceneName;
    [SerializeField] FadeSceneChanger changer;

    bool isSetup;
    bool isAwaken;

    float autoSetupTime;

    private void OnEnable()
    {
        Invoke("Setup", setTime);
    }

    private void Setup()
    {
        isSetup = true;
    }

    public void Update()
    {
        autoSetupTime += Time.deltaTime;

        if (autoSetupTime > 10f && !isAwaken) Gogo();

        if(Input.GetMouseButtonDown(0) && isSetup && !isAwaken)
        {
            Gogo();
        }
    }

    public void Gogo()
    {
        changer.gameObject.SetActive(true);
        changer.Setup(sceneName);
    }

}
