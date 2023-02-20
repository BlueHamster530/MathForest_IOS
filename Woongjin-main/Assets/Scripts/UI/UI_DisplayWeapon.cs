using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_DisplayWeapon : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI tWeaponLeftAmmo;


    public void UpdateWeaponStatus(string wName, int wAmmo, int wLevel)
    {
        if (wAmmo < 0) tWeaponLeftAmmo.text = "x ¹«ÇÑ";
        else tWeaponLeftAmmo.text = "x "+ wAmmo.ToString();
    }
}
