using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileStatus : MonoBehaviour
{
    [SerializeField] private bool isFierce;
    [SerializeField] private int damage;

    public bool     IsFierce => isFierce;
    public int      Damage => damage;

    public void Setup(int damage)
    {
        this.damage = damage;
    }
}
