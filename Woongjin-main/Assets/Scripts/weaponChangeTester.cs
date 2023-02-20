using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponChangeTester : MonoBehaviour
{
    [SerializeField] PlayerAttack pAttack;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            pAttack.WeaponSetup(101);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            pAttack.WeaponSetup(102);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            pAttack.WeaponSetup(103);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            pAttack.WeaponSetup(104);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            pAttack.WeaponSetup(105);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            pAttack.WeaponSetup(106);
        }
    }
}
