using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelQuit : MonoBehaviour
{
    [SerializeField] FadeSceneChanger fs;

    bool kita;

    private void OnEnable()
    {
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;
            kita = true;
        }
        else kita = false;
        
    }

    private void OnDisable()
    {
        if(kita == true) Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GotoMenu()
    {
        Time.timeScale = 1;
        fs.gameObject.SetActive(true);
        fs.Setup("Menu");
    }
}
