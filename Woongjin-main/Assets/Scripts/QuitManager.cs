using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitManager : MonoBehaviour
{
    [SerializeField] GameObject QuitPanel;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            QuitPanel.SetActive(true);
        }
    }
}
