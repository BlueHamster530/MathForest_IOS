using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using Spine.Unity;
using FindHelper;

public class ChallengeSlot : MonoBehaviour
{
    [SerializeField] Sprite[] spritePanelNameLore;
    [SerializeField] Sprite[] imageSuccess;

    [SerializeField] Image imageChal;
    [SerializeField] Image imageSuc;

    [SerializeField] int ChallengeIndex;
    [SerializeField] ChallengeChecker checker;

    [SerializeField] TextMeshProUGUI textChalName;
    [SerializeField] TextMeshProUGUI textChalLore;

    [SerializeField] GameObject noticeGetCostume;
    [SerializeField] GameObject panelGetCostume;
    [SerializeField] string myCostumePrefs;

    [SerializeField] Animator parentAnimator;

    [SerializeField] SkeletonAnimation rewardCharacter;
    [SerializeField] TextMeshProUGUI textCostumeName;

    bool isGetCostume;
    bool isSuccess;

    private void OnEnable()
    {
        RenewSlot();
    }

    public void RenewSlot()
    {
        ChallengeList chal = (ChallengeList)ChallengeIndex;

        if (checker.ChanllgeClearChecker(chal.ToString()) == true)
        {
            imageChal.sprite = spritePanelNameLore[1];
            imageSuc.sprite = imageSuccess[1];

            textChalName.text = checker.ChallengeName[ChallengeIndex];
            textChalLore.text = checker.ChallengeLores[ChallengeIndex];

            isSuccess = true;
        }
        else
        {
            imageChal.sprite    = spritePanelNameLore[0];
            imageSuc.sprite     = imageSuccess[0];

            textChalName.text = "";
            textChalLore.text = "";

            isSuccess = false;
        }

        if (isSuccess && myCostumePrefs != "" && PlayerPrefs.GetInt("skin_" + myCostumePrefs) != 1)
        {
            noticeGetCostume.SetActive(true);
            isGetCostume = true;
        }
        else
        {
            noticeGetCostume.SetActive(false);
            isGetCostume = false;
        }
    }

    public void GetCostume()
    {
        if(isSuccess && myCostumePrefs != "" && PlayerPrefs.GetInt("skin_" + myCostumePrefs) != 1)
        {           
            textCostumeName.text = GetSkinName(myCostumePrefs);

            panelGetCostume.SetActive(true);

            if (rewardCharacter.skeleton == null) rewardCharacter.skeleton = rewardCharacter.GetComponent<SkeletonAnimation>().skeleton;
            
            rewardCharacter.skeleton.SetSkin(myCostumePrefs);
            rewardCharacter.skeleton.SetSlotsToSetupPose();

            PlayerPrefs.SetInt("skin_"+myCostumePrefs, 1);

            RenewSlot();
        }
        
        //parentAnimator.Play("gotoGetCostume");
    }

    public string GetSkinName(string st)
    {
        switch (st)
        {
            case "indian":
                return "원주민 마이노";
            case "pajamas":
                return "파자마 마이노";
            case "rudolph":
                return "루돌프 마이노";
            case "soonshin":
                return "충무공 마이노";
            case "space":
                return "스페이스 마이노";
            case "veteran":
                return "베테랑 마이노";
            case "zombie":
                return "좀비 마이노";
            default:
                return "스킨 획득을 축하합니다!";
            
        }

    }


}
