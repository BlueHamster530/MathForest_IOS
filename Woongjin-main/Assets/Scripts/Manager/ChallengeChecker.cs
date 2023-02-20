using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ChallengeChecker : MonoBehaviour
{
    public bool[] Challenges = new bool[12];

    public string[] ChallengeName = new string[12];
    public string[] ChallengeLores = new string[12];

    [SerializeField] ChallengePanel clearPanel;
    //도전과제 데이터 만들기 귀찮아서 걍 배열로 때렸다

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
