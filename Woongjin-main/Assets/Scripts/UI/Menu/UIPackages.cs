using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPackages : MonoBehaviour
{
    [SerializeField] UIManager_Main uimanager;
    [SerializeField] GameObject myContents_nonUI;

    private void CompleteChange()
    {
        uimanager.UIChangeComplete();
    }

    private void ExitChild()
    {
        uimanager.ExitChild();
    }

    private void ActivateChild()
    {
        if (myContents_nonUI != null)
            uimanager.ActivateMyChild(myContents_nonUI);
        else
        {
            uimanager.isSwappingContents = false;
        }
    }

    private void GotoInfoChild()
    {
        myContents_nonUI.GetComponent<Animator>().Play("content_gotoInfo");

    }

    private void GotoMainChild()
    {
        myContents_nonUI.GetComponent<Animator>().Play("content_gotoMain");
    }
}
