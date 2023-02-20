using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class MapObject_Spine : MonoBehaviour
{
    SkeletonAnimation charAnim;
    string strAnimationName;
    bool bIsTrigger;
    private void Awake()
    {
        charAnim = GetComponentInChildren<SkeletonAnimation>();

        charAnim.AnimationState.TimeScale = 0;
        strAnimationName = charAnim.AnimationName;
        charAnim.AnimationState.SetAnimation(0, strAnimationName, false);

        bIsTrigger = GetComponent<Collider2D>().isTrigger;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (bIsTrigger == false) return;
            charAnim.AnimationState.SetAnimation(0, strAnimationName, false);

        if (TryGetComponent(out TreeParticle tree)) CreateParticle();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (bIsTrigger == true) return;
        charAnim.AnimationState.SetAnimation(0, strAnimationName, false);

        if (TryGetComponent(out TreeParticle tree)) CreateParticle();
    }

    private void CreateParticle()
    {
        Debug.Log(strAnimationName);
        if      (strAnimationName == "tree1-1" || strAnimationName == "tree1-2")    GetComponent<TreeParticle>().CreateParticle(0);
        else if (strAnimationName == "tree2-1" || strAnimationName == "tree2-2")    GetComponent<TreeParticle>().CreateParticle(1);
        else if (strAnimationName == "tree3-1" || strAnimationName == "tree3-2")    GetComponent<TreeParticle>().CreateParticle(2);
    }
}
