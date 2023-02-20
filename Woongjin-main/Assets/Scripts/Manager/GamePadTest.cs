using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePadTest : MonoBehaviour
{
    void Update()
    {
        if(Input.GetAxis("PadHorizontal1") != 0 || Input.GetAxis("PadVertical1") != 0)
        {
            Debug.Log("pad left " + Input.GetAxis("PadHorizontal1")+","+ Input.GetAxis("PadVertical1"));
        }

        
        if (Input.GetAxis("PadHorizontal2") != 0 || Input.GetAxis("PadVertical2") != 0)
        {
            Debug.Log("pad right " + Input.GetAxis("PadHorizontal2") + "," + Input.GetAxis("PadVertical2"));
        }
        
    }
}
