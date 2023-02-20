using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIContents : MonoBehaviour
{
    [SerializeField] UIManager_Main uimanager;
    
    public void StartSwap()
    {
        uimanager.isSwappingContents = true;
    }

    public void EndSwap()
    {
        uimanager.isSwappingContents = false;
    }
}
