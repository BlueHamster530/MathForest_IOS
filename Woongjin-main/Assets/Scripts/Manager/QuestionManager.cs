using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    [SerializeField] WJ_Sample QuestionObject;
    [SerializeField] GameObject MathUi;
    [SerializeField] public Slider DangerUI;
    static public QuestionManager instance;
    // Start is called before the first frame update
    bool bIsQuestionOn;

    TresureBox nowItemBox;

    private void Start()
    {
        instance = this;
        bIsQuestionOn = false;
    }
    public WJ_Sample GetQuestionObject()
    {
        return QuestionObject;
    }
    public void GetQuestion(TresureBox boxinput)
    {
        if (bIsQuestionOn == true) return;

        bIsQuestionOn = true;
        MathUi.SetActive(true);
        QuestionObject.OnClick_MakeQuestion();
        Time.timeScale = 0.0f;
        nowItemBox = boxinput;
    }
    public void QustionIsEnded(bool IsRightAnswer = false)
    {
        if (bIsQuestionOn == false) return;
        bIsQuestionOn = false;
        MathUi.SetActive(false);
        Time.timeScale = 1.0f;
        if (IsRightAnswer == true)
        {
            nowItemBox.QuestionAnswerRight();
        }
        else
        {
            GameObject.FindObjectOfType<EnemyManger>().WorngAnswerCount++;

            DangerUI.value = GameObject.FindObjectOfType<EnemyManger>().WorngAnswerCount / 5.0f;
            nowItemBox.QuestionAnswerWrong();
        }
        nowItemBox = null;
    }

}
