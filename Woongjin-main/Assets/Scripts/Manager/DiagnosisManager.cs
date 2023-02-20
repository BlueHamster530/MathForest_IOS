using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiagnosisManager : MonoBehaviour
{
    WJ_Conn.Learning_Data cLearning;

    [SerializeField] string isDiagnosisChecker = "PlayerDN_State";
    [SerializeField] WJ_Sample smpl;

    [Header("Main Start Button")]
    [SerializeField] Image startButton;
    [SerializeField] Sprite[] buttons;

    [Header("Diagnosis Related")]
    [SerializeField] GameObject panelDiagnosis;
    [SerializeField] Animator diagnosisBackground;

    [SerializeField] GameObject sPanelInfo;
    [SerializeField] GameObject sPanelTest;
    [SerializeField] GameObject sPanelComplete;
    [SerializeField] GameObject sButtonStart;

    [SerializeField] GameObject buttonInitialize;

    [Header("ETC")]
    [SerializeField] UIManager_Main uiManager;
    [SerializeField] GameObject panelMain;
    [SerializeField] GameObject sceneChanger;

    private void Awake()
    {
        RenewButton();
    }

    public void RenewButton()
    {

        if (PlayerPrefs.GetInt(isDiagnosisChecker) == 2)
        {
            startButton.sprite = buttons[1];
        }
        else startButton.sprite = buttons[0];

        buttonInitialize.SetActive(PlayerPrefs.GetInt(isDiagnosisChecker) == 2);
    }

    public void ClickStartButton()
    {
        int a = PlayerPrefs.GetInt(isDiagnosisChecker);

        if (a == 2) GameStart();
        else GotoDiagnosis();
    }

    public void InitializeDiagnosisData()
    {
        PlayerPrefs.SetInt(isDiagnosisChecker, 0);
        smpl.ChangeeState(0);
        RenewButton();
    }

    private void GameStart()
    {
        sceneChanger.SetActive(true);
    }

    private void GotoDiagnosis()
    {
        if (!uiManager.isSwapping && !uiManager.isSwappingContents)
        {
            diagnosisBackground.SetTrigger("goPurple");
            uiManager.UIChange(panelDiagnosis);
        }
            
    }

    public void EndDiagnosis()
    {
        diagnosisBackground.SetTrigger("goOrange");
        uiManager.UIChange(panelMain);

        Invoke("UIInitialize", 1f);
    }

    private void UIInitialize()
    {
        sPanelComplete.SetActive(false);
        sPanelInfo.SetActive(true);
        sButtonStart.SetActive(true);

    }

    public void DNTestEnded()
    {
        //DNStateText.text = "진단평가 완료";
        //MathUI.SetActive(false);
        //DNStartButton.SetActive(false);
        //GameStartButton.SetActive(true);
        sPanelTest.SetActive(false);
        sPanelComplete.SetActive(true);

        PlayerPrefs.SetInt(isDiagnosisChecker, 2);
        PlayerPrefs.SetInt("PlayerDN_State", 2);

        RenewButton();


        //LearningDataManager.instace.SetLearningData(wjSample.GetLearningData());
    }  

    private void QuestionStart()
    {
        
    }
}
