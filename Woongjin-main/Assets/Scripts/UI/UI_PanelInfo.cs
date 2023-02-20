using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_PanelInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textChallengeCount;
    [SerializeField] TextMeshProUGUI textCostumeCount;
    [SerializeField] TextMeshProUGUI textGrade;

    [SerializeField] ChallengeChecker checker;

    private void OnEnable()
    {
        RenewPanel();
    }

    private void RenewPanel()
    {
        textChallengeCount.text = checker.ChallengeClearCount.ToString();
        textCostumeCount.text = GetCostumeCount();

    }

    private string GetCostumeCount()
    {
        int count = 1;

        if (PlayerPrefs.GetInt("skin_indian") == 1) count++;
        if (PlayerPrefs.GetInt("skin_pajamas") == 1) count++;
        if (PlayerPrefs.GetInt("skin_rudolph") == 1) count++;
        if (PlayerPrefs.GetInt("skin_soonshin") == 1) count++;
        if (PlayerPrefs.GetInt("skin_space") == 1) count++;
        if (PlayerPrefs.GetInt("skin_veteran") == 1) count++;
        if (PlayerPrefs.GetInt("skin_zombie") == 1) count++;

        return count.ToString();
    }
}
