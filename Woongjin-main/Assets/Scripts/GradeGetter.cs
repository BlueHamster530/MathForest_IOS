using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GradeGetter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI myText;

    private void Start()
    {
        OnEnable();
    }

    private void OnEnable()
    {
        RenewGrade();
    }

    private void RenewGrade()
    {
        int a = PlayerPrefs.GetInt("RightAnswerCount");

        if (a >= 21)
            myText.text = "모험의 신";
        else if (a >= 18)
            myText.text = "숲의 모험가";
        else if (a >= 15)
            myText.text = "마스터 모험가";
        else if (a >= 12)
            myText.text = "샤이닝 모험가";
        else if (a >= 9)
            myText.text = "플래티넘 모험가";
        else if (a >= 6)
            myText.text = "골드 모험가";
        else if (a >= 3)
            myText.text = "실버 모험가";
        else
            myText.text = "브론즈 모험가";


    }
}
