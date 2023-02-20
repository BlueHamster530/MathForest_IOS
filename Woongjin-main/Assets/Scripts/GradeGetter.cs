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
            myText.text = "������ ��";
        else if (a >= 18)
            myText.text = "���� ���谡";
        else if (a >= 15)
            myText.text = "������ ���谡";
        else if (a >= 12)
            myText.text = "���̴� ���谡";
        else if (a >= 9)
            myText.text = "�÷�Ƽ�� ���谡";
        else if (a >= 6)
            myText.text = "��� ���谡";
        else if (a >= 3)
            myText.text = "�ǹ� ���谡";
        else
            myText.text = "����� ���谡";


    }
}
