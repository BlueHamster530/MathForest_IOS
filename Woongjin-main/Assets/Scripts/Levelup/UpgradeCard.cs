using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using FindHelper;

public class UpgradeCard : MonoBehaviour
{
    public  WeaponTemplate myWeapon;
    private LevelupItemSetter myParent;

    [SerializeField] TextMeshProUGUI textName;
    [SerializeField] TextMeshProUGUI textNextLevel;
    [SerializeField] TextMeshProUGUI textUpgradeContents;
    [SerializeField] Image imageWeapon;

    [SerializeField] Image frameImage;
    [SerializeField] Sprite manlev;

    GameObject player;

    public void Setup(WeaponTemplate weapon, LevelupItemSetter par)
    {
        myParent = par;
        myWeapon = weapon;

        int weaponLevel = GetLevel(myWeapon.nWeaponCode);

        imageWeapon.sprite = ItemFinder.FindItemSprite(myWeapon.nWeaponCode);
        textName.text = myWeapon.sWeaponName;
        textNextLevel.text = $"���� ���� : {(weaponLevel + 1).ToString()}";
        textUpgradeContents.text = GetContents();

        if (weaponLevel + 1 == 3) frameImage.sprite = manlev;

        player = GetComponentInParent<LevelupItemSetter>().player;
    }

    private int GetLevel(int code)
    {
        return PlayData.instance.weaponLevelData[code];
    }  

    private string GetContents()
    {
        string result = "";

        int lv = GetLevel(myWeapon.nWeaponCode) - 1;

        if(myWeapon.optionByLevel[lv].nWeaponDamage != myWeapon.optionByLevel[lv + 1].nWeaponDamage)
        {
            result += $"���ݷ�+" +
                $"{myWeapon.optionByLevel[lv + 1].nWeaponDamage - myWeapon.optionByLevel[lv].nWeaponDamage}";
        }

        if (myWeapon.optionByLevel[lv].nWeaponDamage != myWeapon.optionByLevel[lv + 1].nWeaponDamage &&
            myWeapon.optionByLevel[lv].fWeaponDelay != myWeapon.optionByLevel[lv + 1].fWeaponDelay)
        {
            result += " , ";
        }

        if (myWeapon.optionByLevel[lv].fWeaponDelay != myWeapon.optionByLevel[lv + 1].fWeaponDelay)
        {
            result += $"���� �ӵ�+" +
                $"{((myWeapon.optionByLevel[lv].fWeaponDelay / myWeapon.optionByLevel[lv+1].fWeaponDelay) * 100) - 100} %";
        }

        if(myWeapon.optionByLevel[lv].nWeaponDamage != myWeapon.optionByLevel[lv + 1].nWeaponDamage ||
            myWeapon.optionByLevel[lv].fWeaponDelay != myWeapon.optionByLevel[lv + 1].fWeaponDelay)
        {
            result += "\n";
        }
        if (myWeapon.optionByLevel[lv].nThrowCount != myWeapon.optionByLevel[lv + 1].nThrowCount)
        {
            result += $"�Ѿ�+" +
                $"{myWeapon.optionByLevel[lv + 1].nThrowCount - myWeapon.optionByLevel[lv].nThrowCount} ��";
        }
        if (myWeapon.optionByLevel[lv].nThrowCount != myWeapon.optionByLevel[lv + 1].nThrowCount &&
            myWeapon.optionByLevel[lv].fAimDegree != myWeapon.optionByLevel[lv + 1].fAimDegree)
        {
            result += " , ";
        }
        if (myWeapon.optionByLevel[lv].fAimDegree != myWeapon.optionByLevel[lv + 1].fAimDegree)
        {
            result += $"���� ����+" +
                $"{myWeapon.optionByLevel[lv + 1].fAimDegree - myWeapon.optionByLevel[lv].fAimDegree}";
        }

        if (myWeapon.optionByLevel[lv].nThrowCount != myWeapon.optionByLevel[lv + 1].nThrowCount ||
            myWeapon.optionByLevel[lv].fAimDegree != myWeapon.optionByLevel[lv + 1].fAimDegree)
        {
            result += "\n";
        }

        if (myWeapon.optionByLevel[lv].fAimDistance != myWeapon.optionByLevel[lv + 1].fAimDistance)
        {
            result += $"���� �Ÿ�+" +
                $"{myWeapon.optionByLevel[lv + 1].fAimDistance - myWeapon.optionByLevel[lv].fAimDistance}";
        }

        if (myWeapon.optionByLevel[lv].fAimDistance != myWeapon.optionByLevel[lv + 1].fAimDistance &&
            myWeapon.optionByLevel[lv].gWeaponPrefab != myWeapon.optionByLevel[lv + 1].gWeaponPrefab)
        {
            result += " , ";
        }

        if (myWeapon.optionByLevel[lv].gWeaponPrefab != myWeapon.optionByLevel[lv + 1].gWeaponPrefab)
        {
            result += $"���� ��� ����";
        }


        return result;
    }

    public void WeaponUpgrade()
    {
        if(myWeapon != null)
        {
            PlayData.instance.WeaponLevelUp(myWeapon.nWeaponCode);

            if (myWeapon.nWeaponCode == player.GetComponent<PlayerAttack>().cCurrentWeapon.nWeaponCode)
            {
                player.GetComponent<PlayerAttack>().WeaponSetupWithoutMagazine(myWeapon.nWeaponCode);
            }
        }

        myParent.CardClear();

        WaveManager.instance.LevelUp();
    }

}
