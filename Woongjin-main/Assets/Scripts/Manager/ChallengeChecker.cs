using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ChallengeChecker : MonoBehaviour
{
    public bool[] Challenges = new bool[12];

    public string[] ChallengeName = new string[12];
    public string[] ChallengeLores = new string[12];

    [SerializeField] ChallengePanel clearPanel;
    //�������� ������ ����� �����Ƽ� �� �迭�� ���ȴ�

    public int ChallengeClearCount;

    void Awake()
    {
        int count = 0;
        ChallengeClearCount = 0;

        Challenges[count] = ChanllgeClearChecker("Challenge_First"); count++;
        Challenges[count] = ChanllgeClearChecker("Challenge_Nicetry"); count++;
        Challenges[count] = ChanllgeClearChecker("Challenge_Weaponmaster"); count++;
        Challenges[count] = ChanllgeClearChecker("Challenge_Dragonslayer"); count++;
        Challenges[count] = ChanllgeClearChecker("Challenge_Slimegettodaje"); count++;
        Challenges[count] = ChanllgeClearChecker("Challenge_Firepunch"); count++;
        Challenges[count] = ChanllgeClearChecker("Challenge_Nocrimes"); count++;
        Challenges[count] = ChanllgeClearChecker("Challenge_Basicisnice"); count++;
        Challenges[count] = ChanllgeClearChecker("Challenge_Faker"); count++;
        Challenges[count] = ChanllgeClearChecker("Challenge_Smart"); count++;
        Challenges[count] = ChanllgeClearChecker("Challenge_Thx"); count++;
        Challenges[count] = ChanllgeClearChecker("Challenge_Whoareyou"); count++;

        ChallengeSet();
    }

    

    public bool ChanllgeClearChecker(string _name)
    {
        if (PlayerPrefs.GetInt(_name) == 1)
        {
            ChallengeClearCount++;
            return true;
        }
        return false;
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
