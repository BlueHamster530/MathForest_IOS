using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WJ_Sample : MonoBehaviour
{
    private WJ_Conn scWJ_Conn;
    public GameObject goPopup_Level_Choice;
    public GameObject goGroup_Qustion;
    public TEXDraw txQuestion;
    public Button []btAnsr = new Button[4];
    public TextMeshProUGUI txState;
    public Button btStart;
    protected TEXDraw[]txAnsr;
    private bool IsQuestionShow;
    public bool bIsRightAnswer;

    private float fNoBugTouch;

    [SerializeField] GameObject goRightAnswer;
    [SerializeField] GameObject goWrongAnswer;

    public TextMeshProUGUI leftQuestionCount;

    [SerializeField] DiagnosisManager menu_diagManager;
    bool bIsChallengeMaster;
    protected enum STATE
    {
        DN_SET,         // 진단평가 진행해야 하는 단계
        DN_PROG,        // 진단평가 진행중
        LEARNING,       // 학습 진행중
    }

    protected STATE eState;
    protected bool bRequest;

    protected int nDigonstic_Idx;   // 진단평가 인덱스

    protected WJ_Conn.Learning_Data cLearning;
    protected int nLearning_Idx;     // Learning 문제 인덱스
    protected string[] strQstCransr = new string[8];        // 사용자가 보기에서 선택한 답 내용
    protected long[] nQstDelayTime = new long[8];           // 풀이에 소요된 시간




    // Start is called before the first frame update
    private void OnEnable()
    {
        Awake();
    }
    void Awake()
    {
        if (LearningDataManager.instace == null)
        {
            scWJ_Conn = transform.Find("WJ_Conn").GetComponent<WJ_Conn>();
            eState = (STATE)(PlayerPrefs.GetInt("PlayerDN_State"));
            bIsChallengeMaster = true;
        }
        else
        {
            scWJ_Conn = LearningDataManager.instace.GetWJCOnn();
            eState = (STATE)(PlayerPrefs.GetInt("PlayerDN_State"));
        }
        NativeLeakDetection.Mode = NativeLeakDetectionMode.EnabledWithStackTrace;
        goPopup_Level_Choice.SetActive(false);

        if (eState == STATE.DN_SET)
        {
            cLearning = null;
        }
        else
        {
            cLearning = LearningDataManager.instace.GetLearningData();
        }
        nLearning_Idx = 0;

        if(txState != null) txState.text = "상태 : 진단 평가 미수행";

        txAnsr = new TEXDraw[btAnsr.Length];
        for (int i = 0; i < btAnsr.Length; ++i)
            txAnsr[i] = btAnsr[i].GetComponentInChildren<TEXDraw>();

        SetActive_Question(false);
        if (btStart != null) btStart.gameObject.SetActive(true);
        goGroup_Qustion.SetActive(false);
        bRequest = false;
        IsQuestionShow = false;
        bIsRightAnswer = false;
        fNoBugTouch = 0.3f;
    }

    public WJ_Conn.Learning_Data GetLearningData()
    {
        return cLearning;
    }

    public void ChangeeState(int _value)
    {
        eState = (STATE)_value;
    }

    // 문제 출제 버튼 클릭시 호출
    public void OnClick_MakeQuestion()
    {
        if (IsQuestionShow == false)
        {
            switch (eState)
            {
                case STATE.DN_SET: DoDN_Start(); Debug.Log("DODNSTART"); break;
                //호출 안됨. case STATE.DN_PROG: DoDN_Prog(); break;
                case STATE.LEARNING: DoLearning(); Debug.Log("DOLEARNING"); break;
            }
            IsQuestionShow = true;
        }
    }

    // 학습 수준 선택 팝업에서 사용자가 수준에 맞는 학습을 선택시 호출
    public void OnClick_Level(int _nLevel)
    {
        nDigonstic_Idx = 0;
        if(btStart != null) btStart.gameObject.SetActive(false);

        // 문제 요청
        scWJ_Conn.OnRequest_DN_Setting(_nLevel);

        // 수준 선택 팝업 닫기
        goPopup_Level_Choice.SetActive(false);

        SetActive_Question(true);
        bRequest = true;
    }


    // 보기 선택
    public void OnClick_Ansr(int _nIndex)
    {
        IsQuestionShow = false;
        switch (eState)
        {
            case STATE.DN_SET:
            case STATE.DN_PROG:
                {
                    // 다음문제 출제
                    DoDN_Prog(txAnsr[_nIndex].text);
                }
                break;
            case STATE.LEARNING:
                {
                    // 선택한 정답을 저장함
                    strQstCransr[nLearning_Idx - 1] = txAnsr[_nIndex].text;
                    nQstDelayTime[nLearning_Idx - 1] = 5000;        // 임시값

                    bIsRightAnswer = false;
                    //if (scWJ_Conn.cDiagnotics.data.qstCransr.CompareTo(txAnsr[_nIndex].text) == 0)
                    //{
                    //       QuestionIsAnswer = true;
                    //}
                    if (cLearning.qsts[nLearning_Idx-1].qstCransr.CompareTo(txAnsr[_nIndex].text) == 0)
                    {
                        if (PlayerPrefs.HasKey("RightAnswerCount") == false)
                        {
                            PlayerPrefs.SetInt("RightAnswerCount", 0);
                        }
                        int rightanswercount = PlayerPrefs.GetInt("RightAnswerCount");
                        rightanswercount++;
                        PlayerPrefs.SetInt("RightAnswerCount", rightanswercount);
                        bIsRightAnswer = true;
                    }

                    if (nLearning_Idx >= 8)
                    {
                        DoLearning();
                    }
                    // 다음문제 출제
                    //
                    CloseQustionGroup();
                    QuestionManager.instance.QustionIsEnded(bIsRightAnswer);
                }
                break;
        }
    }

    public void CloseQustionGroup()
    {
        if (btStart != null) btStart.gameObject.SetActive(false);
        goGroup_Qustion.SetActive(false);
    }
    public void GameClear()//8문제 풀이 완료되기 전 게임 종료시 호출
    {
        if (cLearning != null)
        {
            txState.text = "상태 : 학습 완료";
            scWJ_Conn.OnLearningResult(cLearning, strQstCransr, nQstDelayTime);      // 학습 결과 처리
            cLearning = null;
            SetActive_Question(false);
            if (btStart != null) btStart.gameObject.SetActive(false);
            goGroup_Qustion.SetActive(false);
        }
    }



    public void DoDN_Start()
    {
        bIsChallengeMaster = true;
        goPopup_Level_Choice.SetActive(false);
        OnClick_Level(0);
        if(btStart != null) btStart.gameObject.SetActive(false);
    }

    public void DoDN_WongAnswer()
    {

        IsQuestionShow = false;

        strQstCransr[nLearning_Idx - 1] = txAnsr[0].text;
        nQstDelayTime[nLearning_Idx - 1] = 5000;        // 임시값

        bIsRightAnswer = false;
        //if (scWJ_Conn.cDiagnotics.data.qstCransr.CompareTo(txAnsr[_nIndex].text) == 0)
        //{
        //       QuestionIsAnswer = true;
        //}
        //if (cLearning.qsts[nLearning_Idx - 1].qstCransr.CompareTo(txAnsr[0].text) == 0)
        //{
        //    if (PlayerPrefs.HasKey("RightAnswerCount") == false)
        //    {
        //        PlayerPrefs.SetInt("RightAnswerCount", 0);
        //    }
        //    int rightanswercount = PlayerPrefs.GetInt("RightAnswerCount");
        //    rightanswercount++;
        //    PlayerPrefs.SetInt("RightAnswerCount", rightanswercount);
        //    bIsRightAnswer = true;
        //}

        if (nLearning_Idx >= 8)
        {
            DoLearning();
        }
        // 다음문제 출제
        //
        CloseQustionGroup();
        QuestionManager.instance.QustionIsEnded(bIsRightAnswer);
    }
    protected void DoDN_Prog(string _qstCransr)
    {
        if (fNoBugTouch>0) return;

        fNoBugTouch = 0.3f;
        string strYN = "N";
        if (scWJ_Conn.cDiagnotics.data.qstCransr.CompareTo(_qstCransr) == 0)
        {
            if (goRightAnswer != null && goWrongAnswer != null)
            {
                goWrongAnswer.SetActive(false);
                goRightAnswer.SetActive(true);

                if (SystemSoundPlayer.instance != null) SystemSoundPlayer.instance.SystemSoundPlay(SystemSoundList.correct);
            }
            strYN = "Y";
        }
        if (strYN == "N")
        {
            if (goWrongAnswer != null&&goRightAnswer != null)
            {
                goRightAnswer.SetActive(false);
                goWrongAnswer.SetActive(true);

                if (SystemSoundPlayer.instance != null) SystemSoundPlayer.instance.SystemSoundPlay(SystemSoundList.noncorrect);
            }
            bIsChallengeMaster = false;//1문제라도 틀리면 변수 false
        }
        scWJ_Conn.OnRequest_DN_Progress("W",
                                         scWJ_Conn.cDiagnotics.data.qstCd,          // 문제 코드
                                         _qstCransr,                                // 선택한 답내용 -> 사용자가 선택한 문항 데이터 입력
                                         strYN,                                     // 정답여부("Y"/"N")
                                         scWJ_Conn.cDiagnotics.data.sid,            // 문제 SID
                                         5000);                                     // 임시값 - 문제 풀이에 소요된 시간

        bRequest = true;
    }


    protected void DoLearning()
    {
        Debug.Log("러닝실행");

        if (cLearning == null)
        {
            nLearning_Idx = 0;
            scWJ_Conn.OnRequest_Learning();
            SetActive_Question(true);
            btStart.gameObject.SetActive(false);
            goGroup_Qustion.SetActive(true);
            bRequest = true;
        }
        else
        {
            if (nLearning_Idx >= scWJ_Conn.cLearning_Info.data.qsts.Count)
            {
                txState.text = "상태 : 학습 완료";
                scWJ_Conn.OnLearningResult(cLearning, strQstCransr, nQstDelayTime);      // 학습 결과 처리
                cLearning = null;
                SetActive_Question(false);
                btStart.gameObject.SetActive(false);
                goGroup_Qustion.SetActive(false);
                return;
            }


            MakeQuestion(cLearning.qsts[nLearning_Idx].qstCn, cLearning.qsts[nLearning_Idx].qstCransr, cLearning.qsts[nLearning_Idx].qstWransr);
            txState.text = "상태 : 문제 학습 " + (nLearning_Idx + 1).ToString();
            if (leftQuestionCount != null) leftQuestionCount.text = $"{8 - nLearning_Idx}";
            ++nLearning_Idx;

            if (goGroup_Qustion.activeSelf == false)
            {
                goGroup_Qustion.SetActive(true);
            }
            bRequest = false;
        }
    }

    //문제 만드는 함수
    protected void MakeQuestion(string _qstCn, string _qstCransr, string _qstWransr)
    {
        char[] SEP = { ',' };
        string[] tmWrAnswer;
        
        txQuestion.text = scWJ_Conn.GetLatexCode(_qstCn);// 문제 출력

        string strAnswer = _qstCransr;  // 문제 정답을 메모리에 넣어둠                
        tmWrAnswer = _qstWransr.Split(SEP, System.StringSplitOptions.None);   // 오답 리스트
        for(int i = 0; i < tmWrAnswer.Length; ++i)
            tmWrAnswer[i] = scWJ_Conn.GetLatexCode(tmWrAnswer[i]);



        int nWrCount = tmWrAnswer.Length;
        if (nWrCount >= 4)       // 5지선다형 이상은 강제로 4지선다로 변경함
            nWrCount = 3;


        int nAnsrCount = nWrCount + 1;       // 보기 갯수
        for (int i = 0; i < btAnsr.Length; ++i)
        {
            if (i < nAnsrCount)
                btAnsr[i].gameObject.active = true;
            else
                btAnsr[i].gameObject.active = false;
        }


        // 보기 리스트에 정답을 넣음.
        int nAnsridx = UnityEngine.Random.Range(0, nAnsrCount);        // 정답 인덱스! 랜덤으로 배치
        for (int i = 0, q = 0; i < nAnsrCount; ++i, ++q)
        {
            if (i == nAnsridx)
            {
                txAnsr[i].text = strAnswer;
                --q;
            }
            else
                txAnsr[i].text = tmWrAnswer[q];
        }


    }




    protected void SetActive_Question(bool _bActive)
    {
        txQuestion.text = "";
        for (int i = 0; i < btAnsr.Length; ++i)
            btAnsr[i].gameObject.active = _bActive;
    }


    // Update is called once per frame
    void Update()
    {
        if (fNoBugTouch >= 0)
        {
            fNoBugTouch -= Time.unscaledDeltaTime;
        }
        if(bRequest == true && 
           scWJ_Conn.CheckState_Request() == 1)
        {
            switch(eState)
            {
                case STATE.DN_SET:
                    {
                        MakeQuestion(scWJ_Conn.cDiagnotics.data.qstCn, scWJ_Conn.cDiagnotics.data.qstCransr, scWJ_Conn.cDiagnotics.data.qstWransr);

                        if(txState != null) txState.text = "상태 : 진단평가 " + (nDigonstic_Idx + 1).ToString();
                        if (leftQuestionCount != null) leftQuestionCount.text = $"{8 - nDigonstic_Idx}";
                        ++nDigonstic_Idx;
                        if (goGroup_Qustion.activeSelf == false)
                        {
                            goGroup_Qustion.SetActive(true);
                        }
                        eState = STATE.DN_PROG;
                    }
                    break;
                case STATE.DN_PROG:
                    {
                        if (scWJ_Conn.cDiagnotics.data.prgsCd == "E")
                        {
                            SetActive_Question(false);

                            nDigonstic_Idx = 0;
                            if(txState != null) txState.text = "상태 : 진단평가 완료";
                            //btStart.gameObject.active = true;
                            goGroup_Qustion.SetActive(false);
                            eState = STATE.LEARNING;            // 진단 학습 완료
                            PlayerPrefs.GetInt("PlayerDN_State", 2);

                            if (SystemSoundPlayer.instance != null) SystemSoundPlayer.instance.SystemSoundPlay(SystemSoundList.testend);
                            // QuestionManager.instance.QustionIsEnded(true);
                            //print("상태 : 진단평가 완료");
                            if (bIsChallengeMaster == true)
                            {
                                print("진단평가 정답률 100%");
                                if (PlayerPrefs.GetInt("Challenge_Whoareyou") == 0)
                                {
                                    PlayerPrefs.SetInt("Challenge_Whoareyou",1);

                                    if (ChallengeManager.instance != null)
                                    {
                                        ChallengeManager.instance.CreateChallengePanel(ChallengeList.Challenge_Whoareyou);
                                    }
                                }
                            }
                            if(menu_diagManager != null)
                            {
                                //메뉴용 조건문으로 변경
                                menu_diagManager.DNTestEnded();
                            }
                            else
                            {
                                Debug.Log("메뉴화면에서 진단평가 안했거나, 옵젝연결안됨");
                            }
                            /*
                            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MathForest")
                            {//현재 씬이 MathForest 라면(게임시작전 진단평가 결과를 위한 조건)
                                GameMainSceneManager.instance.DNTestEnded();
                            }
                            else
                            {
                                QuestionManager.instance.QustionIsEnded(true);
                            }
                            */
                        }
                        else
                        {
                            MakeQuestion(scWJ_Conn.cDiagnotics.data.qstCn, scWJ_Conn.cDiagnotics.data.qstCransr, scWJ_Conn.cDiagnotics.data.qstWransr);

                            if (txState != null) txState.text = "상태 : 진단평가 " + (nDigonstic_Idx + 1).ToString();
                            if (leftQuestionCount != null) leftQuestionCount.text = $"{8 - nDigonstic_Idx}";
                            ++nDigonstic_Idx;
                        }
                    }
                    break;
                case STATE.LEARNING:
                    {
                        cLearning = scWJ_Conn.cLearning_Info.data;
                        MakeQuestion(cLearning.qsts[nLearning_Idx].qstCn, cLearning.qsts[nLearning_Idx].qstCransr, cLearning.qsts[nLearning_Idx].qstWransr);
                        //txState.text = "상태 : 문제 학습 " + (nLearning_Idx + 1).ToString();
                        ++nLearning_Idx;                        
                    }
                    break;
            }
            bRequest = false;
        }        
    }
}
