using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDeactive : MonoBehaviour
{
    [SerializeField] bool isTimeLimit;
    [SerializeField] float exitTime;

    private void Awake()
    {
        if (isTimeLimit) Invoke("ExitSelf", exitTime);
    }

    private void ExitSelf()
    {
        gameObject.SetActive(false);
    }
}
