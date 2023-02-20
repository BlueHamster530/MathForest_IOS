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
        DN_SET,         // ������ �����ؾ� �ϴ� �ܰ�
        DN_PROG,        // ������ ������
        LEARNING,       // �н� ������
    }

    protected STATE eState;
    protected bool bRequest;

    protected int nDigonstic_Idx;   // ������ �ε���

    protected WJ_Conn.Learning_Data cLearning;
    protected int nLearning_Idx;     // Learning ���� �ε���
    protected string[] strQstCransr = new string[8];        // ����ڰ� ���⿡�� ������ �� ����
    protected long[] nQstDelayTime = new long[8];           // Ǯ�̿� �ҿ�� �ð�




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

        if(txState != null) txState.text = "���� : ���� �� �̼���";

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

    // ���� ���� ��ư Ŭ���� ȣ��
    public void OnClick_MakeQuestion()
    {
        if (IsQuestionShow == false)
        {
            switch (eState)
            {
                case STATE.DN_SET: DoDN_Start(); Debug.Log("DODNSTART"); break;
                //ȣ�� �ȵ�. case STATE.DN_PROG: DoDN_Prog(); break;
                case STATE.LEARNING: DoLearning(); Debug.Log("DOLEARNING"); break;
            }
            IsQuestionShow = true;
        }
    }

    // �н� ���� ���� �˾����� ����ڰ� ���ؿ� �´� �н��� ���ý� ȣ��
    public void OnClick_Level(int _nLevel)
    {
        nDigonstic_Idx = 0;
        if(btStart != null) btStart.gameObject.SetActive(false);

        // ���� ��û
        scWJ_Conn.OnRequest_DN_Setting(_nLevel);

        // ���� ���� �˾� �ݱ�
        goPopup_Level_Choice.SetActive(false);

        SetActive_Question(true);
        bRequest = true;
    }


    // ���� ����
    public void OnClick_Ansr(int _nIndex)
    {
        IsQuestionShow = false;
        switch (eState)
        {
            case STATE.DN_SET:
            case STATE.DN_PROG:
                {
                    // �������� ����
                    DoDN_Prog(txAnsr[_nIndex].text);
                }
                break;
            case STATE.LEARNING:
                {
                    // ������ ������ ������
                    strQstCransr[nLearning_Idx - 1] = txAnsr[_nIndex].text;
                    nQstDelayTime[nLearning_Idx - 1] = 5000;        // �ӽð�

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
                    // �������� ����
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
    public void GameClear()//8���� Ǯ�� �Ϸ�Ǳ� �� ���� ����� ȣ��
    {
        if (cLearning != null)
        {
            txState.text = "���� : �н� �Ϸ�";
            scWJ_Conn.OnLearningResult(cLearning, strQstCransr, nQstDelayTime);      // �н� ��� ó��
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
        nQstDelayTime[nLearning_Idx - 1] = 5000;        // �ӽð�

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
        // �������� ����
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
            bIsChallengeMaster = false;//1������ Ʋ���� ���� false
        }
        scWJ_Conn.OnRequest_DN_Progress("W",
                                         scWJ_Conn.cDiagnotics.data.qstCd,          // ���� �ڵ�
                                         _qstCransr,                                // ������ �䳻�� -> ����ڰ� ������ ���� ������ �Է�
                                         strYN,                                     // ���俩��("Y"/"N")
                                         scWJ_Conn.cDiagnotics.data.sid,            // ���� SID
                                         5000);                                     // �ӽð� - ���� Ǯ�̿� �ҿ�� �ð�

        bRequest = true;
    }


    protected void DoLearning()
    {
        Debug.Log("���׽���");

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
                txState.text = "���� : �н� �Ϸ�";
                scWJ_Conn.OnLearningResult(cLearning, strQstCransr, nQstDelayTime);      // �н� ��� ó��
                cLearning = null;
                SetActive_Question(false);
                btStart.gameObject.SetActive(false);
                goGroup_Qustion.SetActive(false);
                return;
            }


            MakeQuestion(cLearning.qsts[nLearning_Idx].qstCn, cLearning.qsts[nLearning_Idx].qstCransr, cLearning.qsts[nLearning_Idx].qstWransr);
            txState.text = "���� : ���� �н� " + (nLearning_Idx + 1).ToString();
            if (leftQuestionCount != null) leftQuestionCount.text = $"{8 - nLearning_Idx}";
            ++nLearning_Idx;

            if (goGroup_Qustion.activeSelf == false)
            {
                goGroup_Qustion.SetActive(true);
            }
            bRequest = false;
        }
    }

    //���� ����� �Լ�
    protected void MakeQuestion(string _qstCn, string _qstCransr, string _qstWransr)
    {
        char[] SEP = { ',' };
        string[] tmWrAnswer;
        
        txQuestion.text = scWJ_Conn.GetLatexCode(_qstCn);// ���� ���

        string strAnswer = _qstCransr;  // ���� ������ �޸𸮿� �־��                
        tmWrAnswer = _qstWransr.Split(SEP, System.StringSplitOptions.None);   // ���� ����Ʈ
        for(int i = 0; i < tmWrAnswer.Length; ++i)
            tmWrAnswer[i] = scWJ_Conn.GetLatexCode(tmWrAnswer[i]);



        int nWrCount = tmWrAnswer.Length;
        if (nWrCount >= 4)       // 5�������� �̻��� ������ 4�����ٷ� ������
            nWrCount = 3;


        int nAnsrCount = nWrCount + 1;       // ���� ����
        for (int i = 0; i < btAnsr.Length; ++i)
        {
            if (i < nAnsrCount)
                btAnsr[i].gameObject.active = true;
            else
                btAnsr[i].gameObject.active = false;
        }


        // ���� ����Ʈ�� ������ ����.
        int nAnsridx = UnityEngine.Random.Range(0, nAnsrCount);        // ���� �ε���! �������� ��ġ
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

                        if(txState != null) txState.text = "���� : ������ " + (nDigonstic_Idx + 1).ToString();
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
                            if(txState != null) txState.text = "���� : ������ �Ϸ�";
                            //btStart.gameObject.active = true;
                            goGroup_Qustion.SetActive(false);
                            eState = STATE.LEARNING;            // ���� �н� �Ϸ�
                            PlayerPrefs.GetInt("PlayerDN_State", 2);

                            if (SystemSoundPlayer.instance != null) SystemSoundPlayer.instance.SystemSoundPlay(SystemSoundList.testend);
                            // QuestionManager.instance.QustionIsEnded(true);
                            //print("���� : ������ �Ϸ�");
                            if (bIsChallengeMaster == true)
                            {
                                print("������ ����� 100%");
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
                                //�޴��� ���ǹ����� ����
                                menu_diagManager.DNTestEnded();
                            }
                            else
                            {
                                Debug.Log("�޴�ȭ�鿡�� ������ ���߰ų�, ��������ȵ�");
                            }
                            /*
                            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MathForest")
                            {//���� ���� MathForest ���(���ӽ����� ������ ����� ���� ����)
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

                            if (txState != null) txState.text = "���� : ������ " + (nDigonstic_Idx + 1).ToString();
                            if (leftQuestionCount != null) leftQuestionCount.text = $"{8 - nDigonstic_Idx}";
                            ++nDigonstic_Idx;
                        }
                    }
                    break;
                case STATE.LEARNING:
                    {
                        cLearning = scWJ_Conn.cLearning_Info.data;
                        MakeQuestion(cLearning.qsts[nLearning_Idx].qstCn, cLearning.qsts[nLearning_Idx].qstCransr, cLearning.qsts[nLearning_Idx].qstWransr);
                        //txState.text = "���� : ���� �н� " + (nLearning_Idx + 1).ToString();
                        ++nLearning_Idx;                        
                    }
                    break;
            }
            bRequest = false;
        }        
    }
}
