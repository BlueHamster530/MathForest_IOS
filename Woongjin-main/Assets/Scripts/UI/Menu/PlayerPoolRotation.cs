using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoolRotation : MonoBehaviour
{
    CostumeManager costumeManager;

    private void Awake()
    {
        costumeManager = GetComponentInParent<CostumeManager>();    
    }

    public void RotateComplete(int a)
    {
        costumeManager.Rotate_SkinChange(a);
    }
}
