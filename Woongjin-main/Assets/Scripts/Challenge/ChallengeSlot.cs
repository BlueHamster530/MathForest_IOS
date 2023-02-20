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
                return "���ֹ� ���̳�";
            case "pajamas":
                return "���ڸ� ���̳�";
            case "rudolph":
                return "�絹�� ���̳�";
            case "soonshin":
                return "�湫�� ���̳�";
            case "space":
                return "�����̽� ���̳�";
            case "veteran":
                return "���׶� ���̳�";
            case "zombie":
                return "���� ���̳�";
            default:
                return "��Ų ȹ���� �����մϴ�!";
            
        }

    }


}
