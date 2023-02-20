using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChallengePanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textChalName;

    public void Setup(string name)
    {
        textChalName.text = name;
    }

    public void InstantExit()
    {
        GetComponent<Animator>().Play("chal_exit");
    }
}
