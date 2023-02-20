using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChallengeList
{
    Challenge_First = 0,
    Challenge_Nicetry,
    Challenge_Weaponmaster,
    Challenge_Dragonslayer,
    Challenge_Slimegettodaje,
    Challenge_Firepunch,
    Challenge_Nocrimes,
    Challenge_Basicisnice,
    Challenge_Faker,
    Challenge_Smart,
    Challenge_Thx,
    Challenge_Whoareyou
}

public class ChallengeManager : MonoBehaviour
{
    public static ChallengeManager instance;

    private bool[] SlimeKillType = new bool[6];
    public string[] ChallengeName = new string[12];
    public string[] ChallengeLores = new string[12];


    public bool bIsPlayerAttacked;
    public bool bIsPlayerWeaponChanged;

    [SerializeField] ChallengePanel cPanel;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        bIsPlayerAttacked = false;
        bIsPlayerWeaponChanged = false;

        ChallengeSet();
    }
    public void PlayerAttackedCheckFunction()
    {
        if (bIsPlayerAttacked == false)
        {
            if (PlayerPrefs.GetInt("Challenge_Nocrimes") == 0)
            {
                PlayerPrefs.SetInt("Challenge_Nocrimes", 1);
            }
        }
    }
    public void PlayerBasicWeaponClearCheck()
    {
        if (bIsPlayerWeaponChanged == false)
        {
            if (PlayerPrefs.GetInt("Challenge_Basicisnice") == 0)
            {
                PlayerPrefs.SetInt("Challenge_Basicisnice", 1);
            }
        }
    }
    public void KillSlime(int _type)
    {
        if (PlayerPrefs.GetInt("Challenge_Slimegettodaje") != 0) return;

        if (_type < 0 || _type >= 6)
            print("_type���� �̻��ѵ�" + _type);

        Mathf.Clamp(_type, 0, 5);

        SlimeKillType[_type] = true;

        bool AllKill = true;
        for (int i = 0; i < 6; i++)
        {
            if (SlimeKillType[_type] == false)
            {
                AllKill = false;
            }
        }
        if (AllKill == true)
        {
            PlayerPrefs.SetInt("Challenge_Slimegettodaje", 1);

            if (ChallengeManager.instance != null)
            {
                ChallengeManager.instance.CreateChallengePanel(ChallengeList.Challenge_Slimegettodaje);
            }
        }
    }

    public void CreateChallengePanel(ChallengeList list)
    {
        ChallengePanel cP = Instantiate(cPanel, transform.position, Quaternion.identity);
        cP.transform.SetParent(GameObject.Find("Canvas").transform);
        cP.GetComponent<RectTransform>().localScale = Vector3.one;

        if (PlayerPrefs.GetInt("Challenge_Thx") != 0)
        {
            PlayerPrefs.SetInt("Challenge_Thx", 1);
        }
        cP.Setup(ChallengeName[(int)list]);
    }

    private void ChallengeSet()
    {
        ChallengeName[0] = "ù ��° ����";
        ChallengeName[1] = "������ ����߾�";
        ChallengeName[2] = "���� ������";
        ChallengeName[3] = "�� ��ɲ�";
        ChallengeName[4] = "��ü������ ����";
        ChallengeName[5] = "������!";
        ChallengeName[6] = "������ ������";
        ChallengeName[7] = "�⺻�� �����";
        ChallengeName[8] = "������ ������";
        ChallengeName[9] = "���¾� �ȶ���";
        ChallengeName[10] = "�����մϴ�";
        ChallengeName[11] = "Ȥ�� ���п�?";

        ChallengeLores[0] = "���� 1�� �÷���";
        ChallengeLores[1] = "���� ���� 1ȸ";
        ChallengeLores[2] = "���� 1�� �ִ� ���� �޼�";
        ChallengeLores[3] = "���� 1ȸ Ŭ����";
        ChallengeLores[4] = "�� ���ӿ��� ��� ������ ������ óġ";
        ChallengeLores[5] = "������ �� �������� ���";
        ChallengeLores[6] = "�������� �ʰ� ù��° ���̺� Ŭ����";
        ChallengeLores[7] = "�⺻ ����� ���� Ŭ����";
        ChallengeLores[8] = "�ƹ� ��(�⺻ �� ����)���� ���� ����";
        ChallengeLores[9] = "���� ���͸� ���� �ʰ� ���� Ŭ����";
        ChallengeLores[10] = "ũ���� Ŭ��";
        ChallengeLores[11] = "�����򰡸� ��� ���߱�";
    }
}
