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
            print("_type값이 이상한데" + _type);

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
        ChallengeName[0] = "첫 번째 도전";
        ChallengeName[1] = "괜찮아 노력했어";
        ChallengeName[2] = "무기 전문가";
        ChallengeName[3] = "용 사냥꾼";
        ChallengeName[4] = "액체괴물은 내꺼";
        ChallengeName[5] = "불조심!";
        ChallengeName[6] = "폭력은 나빠요";
        ChallengeName[7] = "기본을 충실히";
        ChallengeName[8] = "단정한 옷차림";
        ChallengeName[9] = "나는야 똑똑이";
        ChallengeName[10] = "감사합니다";
        ChallengeName[11] = "혹시 수학왕?";

        ChallengeLores[0] = "게임 1번 플레이";
        ChallengeLores[1] = "게임 오버 1회";
        ChallengeLores[2] = "무기 1개 최대 레벨 달성";
        ChallengeLores[3] = "게임 1회 클리어";
        ChallengeLores[4] = "한 게임에서 모든 종류의 슬라임 처치";
        ChallengeLores[5] = "보스의 불 공격으로 사망";
        ChallengeLores[6] = "공격하지 않고 첫번째 웨이브 클리어";
        ChallengeLores[7] = "기본 무기로 게임 클리어";
        ChallengeLores[8] = "아무 옷(기본 옷 제외)으로 게임 시작";
        ChallengeLores[9] = "유령 몬스터를 보지 않고 게임 클리어";
        ChallengeLores[10] = "크레딧 클릭";
        ChallengeLores[11] = "진단평가를 모두 맞추기";
    }
}
