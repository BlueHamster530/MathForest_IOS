using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameMainSceneManager : MonoBehaviour
{
    [SerializeField] GameObject GameStartButton;
    [SerializeField] GameObject DNStartButton;
    [SerializeField] GameObject DNResetButton;
    [SerializeField] GameObject MathUI;
    [SerializeField] TextMeshProUGUI DNStateText;
    
    WJ_Sample wjSample;
    int PlayerDN_State;
    static public GameMainSceneManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        GameStartButton .SetActive(false);
        DNStartButton   .SetActive(false);
        MathUI.SetActive(false);
        PlayerPrefs.SetInt("PlayerDN_State", 0);
        PlayerDN_State = PlayerPrefs.GetInt("PlayerDN_State");
        if (PlayerDN_State == 0)
        {
            DNStartButton.SetActive(true);
            DNStateText.text = "진단평가 필요";
        }
        else
        {
            GameStartButton.SetActive(true);
            DNStateText.text = "진단평가 완료";
        }
        wjSample = MathUI.GetComponent<WJ_Sample>();
    }
    public void DNStartButtonClick()
    {
        MathUI.SetActive(true);
        wjSample.OnClick_MakeQuestion();
    }
    public void DNTestEnded()
    {
        DNStateText.text = "진단평가 완료";
        MathUI.SetActive(false);
        DNStartButton.SetActive(false);
        GameStartButton.SetActive(true);
        PlayerPrefs.SetInt("PlayerDN_State", 2);
        LearningDataManager.instace.SetLearningData(wjSample.GetLearningData());
    }
    public void DNTestInfomationReset()
    {
        PlayerPrefs.SetInt("PlayerDN_State", 0);
        wjSample.ChangeeState(0);
        DNStateText.text = "진단평가 필요";
        DNStartButton.SetActive(true);
        GameStartButton.SetActive(false);
    }
}
