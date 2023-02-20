using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;
using UnityEngine.UI;
using FindHelper;
using TMPro;    


public class CostumeManager : MonoBehaviour
{
    [SerializeField] Animator characterPools;
    [SerializeField] string[] skinLists;
    [SerializeField] int[] skinIndex;

    [SerializeField] SkeletonAnimation[] charLists;

    [SerializeField] Image buttonEquip;
    [SerializeField] Sprite[] buttonEquipSprites;

    [SerializeField] TextMeshProUGUI textSkinName, textSkinLore, textSkinCondition;

    [SerializeField] SpriteRenderer[] charSkinLockImages;
    [SerializeField] Sprite[] lockSprites;

    [SerializeField] GameObject[] hands;

    private void Start()
    {
        Setup();
        OnEnable();
    }

    public void OnEnable()
    {
        UpdateButton();
        RenewSkin();
    }

    public void RotateCharacterPools(int a)
    {
        if (a == 1) characterPools.Play("MoveRight");
        else characterPools.Play("MoveLeft");
    }

    public void Rotate_SkinChange(int a)
    {
        if(a == 1)//오른쪽으로 한칸씩 이동
        {
            int temp = skinIndex[skinIndex.Length - 1];

            for (int i = skinIndex.Length - 2; i >= 0; i--)
            {
                skinIndex[i+1] = skinIndex[i];
            }
            skinIndex[0] = temp;
        }
        else if(a == -1)
        {
            int temp = skinIndex[0];

            for (int i = 0; i< skinIndex.Length - 1; i++)
            {
                skinIndex[i] = skinIndex[i + 1];
            }
            skinIndex[skinIndex.Length - 1] = temp;           
        }

        for (int i = 0; i < charLists.Length; i++)
        {
            if (PlayerPrefs.GetInt("skin_" + skinLists[skinIndex[i]]) == 1 || skinLists[skinIndex[i]] == "basic")
            {
                hands[i].SetActive(true);

                charLists[i].GetComponent<MeshRenderer>().enabled = true;
                charSkinLockImages[i].gameObject.SetActive(false);

                charLists[i].skeleton.SetSkin(skinLists[skinIndex[i]]);
                Debug.Log($"char {i} to Set {skinIndex[i]}");
                charLists[i].skeleton.SetSlotsToSetupPose();

                
            }
            else
            {
                hands[i].SetActive(false);

                charLists[i].GetComponent<MeshRenderer>().enabled = false;
                charSkinLockImages[i].gameObject.SetActive(true);

                charSkinLockImages[i].sprite = lockSprites[skinIndex[i]];

                
            }
            
        }

        UpdateButton();
    }

    public void RenewSkin()
    {
        for (int i = 0; i < charLists.Length; i++)
        {
            if (PlayerPrefs.GetInt("skin_" + skinLists[skinIndex[i]]) == 1 || skinLists[skinIndex[i]] == "basic")
            {
                hands[i].SetActive(true);

                charLists[i].GetComponent<MeshRenderer>().enabled = true;
                charSkinLockImages[i].gameObject.SetActive(false);

                charLists[i].skeleton = charLists[i].GetComponent<SkeletonAnimation>().skeleton;

                if (charLists[i].skeleton != null)
                {
                    charLists[i].skeleton.SetSkin(skinLists[skinIndex[i]]);

                    charLists[i].skeleton.SetSlotsToSetupPose();

                    Debug.Log(i + "번 스킨있다");
                }
                    


                    

            }
            else
            {
                hands[i].SetActive(false);

                charLists[i].GetComponent<MeshRenderer>().enabled = false;
                charSkinLockImages[i].gameObject.SetActive(true);

                charSkinLockImages[i].sprite = lockSprites[skinIndex[i]];

                Debug.Log(i + "번 스킨없당");
            }

        }
    }

    public void UpdateButton()
    {
        if(PlayerPrefs.GetInt("skin_" + skinLists[skinIndex[2]]) == 1 || skinLists[skinIndex[2]] == "basic")
        {
            buttonEquip.GetComponent<Button>().interactable = true;
            textSkinName.text = SkinFinder.FindSkin(1000 + skinIndex[2]).skinName;
            textSkinLore.text = SkinFinder.FindSkin(1000 + skinIndex[2]).skinLore;
            textSkinCondition.text = SkinFinder.FindSkin(1000 + skinIndex[2]).skinCondition;
        }
        else
        {
            buttonEquip.GetComponent<Button>().interactable = false;
            textSkinName.text = "스킨 미획득";
            textSkinLore.text = "-";
            textSkinCondition.text = "도전과제 달성하여 잠금 해제";
        }

        if (PlayerPrefs.GetString("PlayerSkin") != skinLists[skinIndex[2]])
        {
            buttonEquip.sprite = buttonEquipSprites[0];
        }
        else
        {
            buttonEquip.sprite = buttonEquipSprites[1];
        }


        

    }

    public void Setup()
    {
        for(int i=0; i<charLists.Length; i++)
        {
            charLists[i].skeleton.SetSkin(skinLists[skinIndex[i]]);
            charLists[i].skeleton.SetSlotsToSetupPose();
            Debug.Log(i % skinLists.Length);
        }
    }

    public void SelectCostume()
    {
        string str = skinLists[skinIndex[2]];
        PlayerPrefs.SetString("PlayerSkin", str);
        PlayerPrefs.SetInt("PlayerSkinIndex", skinIndex[2]);

        UpdateButton();
    }
}
