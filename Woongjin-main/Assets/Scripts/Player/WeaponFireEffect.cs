using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFireEffect : MonoBehaviour
{
    Transform target;
    Vector3 goalDir;
    Vector3 goDir;

    bool isSetup;
    float deg;

    public void Setup(Transform target)
    {
        this.target = target;

        if (target == null) return;

        goalDir = target.position - transform.position;

        deg = Mathf.Atan2(goalDir.y, goalDir.x);

        transform.rotation = Quaternion.Euler(0, 0, deg * Mathf.Rad2Deg);

        goDir.x = Mathf.Cos(deg);
        goDir.y = Mathf.Sin(deg);
    }
}
