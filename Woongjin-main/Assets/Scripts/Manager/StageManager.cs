using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public VirtualPad gPadMove;
    public VirtualPad gPadAim;

    [SerializeField] UI_DisplayWeapon displayWeapon;


    public void UpdateDisplayWeapon(string wName, int wAmmo, int wLevel)
    {
        if(displayWeapon != null) displayWeapon.UpdateWeaponStatus(wName, wAmmo, wLevel);
    }

}
