using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMap : MonoBehaviour
{
    [SerializeField] BGMChanger changer;

    private void OnEnable()
    {
        changer.BGMChange(0);
    }
}
