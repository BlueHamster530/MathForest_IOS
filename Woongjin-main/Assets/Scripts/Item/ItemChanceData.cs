using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ItemChanceData
{
    public int code;
    [Range(0, 100)] 
    public int chance;

    [HideInInspector]
    public int index;
    [HideInInspector]
    public int weight;


}
