using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChangeTester : MonoBehaviour
{
    [SerializeField] int[] weaponList;
    PlayerAttack pAttack;

    private void Awake()
    {
        pAttack = GetComponent<PlayerAttack>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) pAttack.WeaponSetup(weaponList[0]);
        if (Input.GetKeyDown(KeyCode.Alpha2)) pAttack.WeaponSetup(weaponList[1]);
        if (Input.GetKeyDown(KeyCode.Alpha3)) pAttack.WeaponSetup(weaponList[2]);
        if (Input.GetKeyDown(KeyCode.Alpha4)) pAttack.WeaponSetup(weaponList[3]);
        if (Input.GetKeyDown(KeyCode.Alpha5)) pAttack.WeaponSetup(weaponList[4]);
        if (Input.GetKeyDown(KeyCode.Alpha6)) pAttack.WeaponSetup(weaponList[5]);
        if (Input.GetKeyDown(KeyCode.Alpha7)) pAttack.WeaponSetup(weaponList[6]);

        if (Input.GetKeyDown(KeyCode.F1)) PlayData.instance.WeaponLevelUp(weaponList[0]);
        if (Input.GetKeyDown(KeyCode.F2)) PlayData.instance.WeaponLevelUp(weaponList[1]);
        if (Input.GetKeyDown(KeyCode.F3)) PlayData.instance.WeaponLevelUp(weaponList[2]);
        if (Input.GetKeyDown(KeyCode.F4)) PlayData.instance.WeaponLevelUp(weaponList[3]);
        if (Input.GetKeyDown(KeyCode.F5)) PlayData.instance.WeaponLevelUp(weaponList[4]);
        if (Input.GetKeyDown(KeyCode.F6)) PlayData.instance.WeaponLevelUp(weaponList[5]);
        if (Input.GetKeyDown(KeyCode.F7))
        {
            PlayData.instance.WeaponLevelUp(weaponList[6]);
            Debug.Log("케겔운동 완료");
        }




    }
}
