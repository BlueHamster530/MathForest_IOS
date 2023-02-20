using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPrefab : MonoBehaviour
{
    public Transform ShootPos;
    public Transform BackPos;

    public bool isDirectionFix;

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        anim.keepAnimatorControllerStateOnDisable = true;
    }

    private void OnEnable()
    {
        anim = GetComponent<Animator>();
        anim.keepAnimatorControllerStateOnDisable = true;
    }
}
