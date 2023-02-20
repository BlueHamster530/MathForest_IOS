using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayData : MonoBehaviour
{
    public int maxWeaponLevel;
    public static PlayData instance;

    public Dictionary<int, int> weaponLevelData;

    public int enemyKillCount;

    private PlayData()
    {
        instance = this;
    }

    private void Awake()
    {
        weaponLevelData = new Dictionary<int, int>();
        Initialize();
    }

    private void Initialize()
    {
        for(int i=101; i<108; i++)
        {
            weaponLevelData.Add(i, 1);
        }
    }

    public void WeaponLevelUp(int code)
    {
        weaponLevelData[code] += 1;
        if (PlayerPrefs.GetInt("Challenge_Weaponmaster") == 0)
        {
            if (weaponLevelData[code] >= maxWeaponLevel)
            {
                PlayerPrefs.SetInt("Challenge_Weaponmaster", 1);

                if (ChallengeManager.instance != null)
                {
                    ChallengeManager.instance.CreateChallengePanel(ChallengeList.Challenge_Weaponmaster);
                }
            }
        }
        weaponLevelData[code] = Mathf.Clamp(weaponLevelData[code], 1, maxWeaponLevel);
    }
}
