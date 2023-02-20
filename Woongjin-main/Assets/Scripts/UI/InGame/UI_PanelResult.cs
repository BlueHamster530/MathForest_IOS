using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class UI_PanelResult : MonoBehaviour
{
    [SerializeField] StageManager stageManager;
    [SerializeField] WaveManager waveManager;

    [SerializeField] CinemachineVirtualCamera playerCam;

    [Header("Contents")]
    [SerializeField] GameObject[] panels;
    [SerializeField] TextMeshProUGUI textPlayTime;
    [SerializeField] TextMeshProUGUI textHuntCount;
    [SerializeField] TextMeshProUGUI textWaveCount;

    bool isActivated;

    int setValue;

    public void PanelSetup(int a)
    {
        if (a == 0) panels[0].SetActive(true);
        else panels[1].SetActive(true);

        textPlayTime.text = $"{(int)waveManager.playTotalTime / 60}ºÐ {(int)waveManager.playTotalTime % 60}ÃÊ";
        textHuntCount.text = $"{(int)waveManager.huntCount}";
        textWaveCount.text = $"{(int)waveManager.currentWave}";

        Debug.Log("ÇÏ ¾¾¹ß");
    }
 
}
