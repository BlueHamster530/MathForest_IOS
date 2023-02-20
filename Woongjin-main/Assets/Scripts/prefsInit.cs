using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prefsInit : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F12))
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
