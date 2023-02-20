using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_GradeSlot : MonoBehaviour
{
    [SerializeField] Image uiGrade;
    [SerializeField] Image uiArrow;

    [SerializeField] Sprite[] grade;
    [SerializeField] Sprite[] arrow;

    [SerializeField] int requireAnswerCount;

    private void Awake()
    {
        if(PlayerPrefs.GetInt("RightAnswerCount") >= requireAnswerCount)
        {
            uiGrade.sprite = grade[1];
            uiArrow.sprite = arrow[1];
        }
        else
        {
            uiGrade.sprite = grade[0];
            uiArrow.sprite = arrow[0];
        }

    }

    private void OnEnable()
    {
        Awake();
    }
}
