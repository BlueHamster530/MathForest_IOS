using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "CreateTemplate/Create Skin Data", fileName = "NewSkinData")]
public class SkinData : ScriptableObject
{
    public string skinName;
    public string skinLore;
    public string skinCondition;

}
