using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_Main : MonoBehaviour
{
    [SerializeField] GameObject uiPackage_Main;
    [SerializeField] GameObject uiPackage_Costume;

    [SerializeField] GameObject selectedObj;
    [SerializeField] GameObject selectedObj_NonUI;

    GameObject waitingNewObj;
    public bool isSwapping;
    public bool isSwappingContents;

    Animator anim;
    
    public void UIChange(GameObject newObj)
    {
        if(!isSwapping && !isSwappingContents)
        {
            isSwapping = true;

            waitingNewObj = newObj;
            if (selectedObj.TryGetComponent<Animator>(out anim)) anim.SetTrigger("Exit");
            else
            {
                UIChangeComplete();
            }
        }      
    }

    public void UIChangeComplete()
    {
        selectedObj.SetActive(false);

        waitingNewObj.SetActive(true);
        selectedObj = waitingNewObj;

        isSwapping = false;
    }

    public void ExitChild()
    {
        selectedObj_NonUI.GetComponent<Animator>().SetTrigger("exit");
    }

    public void ActivateMyChild(GameObject newContent)
    {
        if (selectedObj_NonUI == newContent) return;

        selectedObj_NonUI.SetActive(false);

        newContent.SetActive(true);

        selectedObj_NonUI = newContent;
    }
}
