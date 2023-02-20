using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class CostumeChangeTest : MonoBehaviour
{
    [SerializeField] int maxSkinIndex = 3;
    SkeletonAnimation skel;

    int currentSkinIndex;

    void Start()
    {
        skel = GetComponent<SkeletonAnimation>();
    }

    public void SkinChange()
    {
        currentSkinIndex++;
        if(currentSkinIndex >= maxSkinIndex) currentSkinIndex = 0;

        switch (currentSkinIndex)
        {
            case 0:
                skel.skeleton.SetSkin("basic");
                skel.skeleton.SetSlotsToSetupPose();
                break;
            case 1:
                skel.skeleton.SetSkin("pajamas");
                skel.skeleton.SetSlotsToSetupPose();
                break;
            case 2:
                skel.skeleton.SetSkin("zombie");
                skel.skeleton.SetSlotsToSetupPose();
                break;
        }

    }
}
