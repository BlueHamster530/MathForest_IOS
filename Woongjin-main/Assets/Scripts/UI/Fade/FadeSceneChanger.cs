using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FadeSceneChanger : MonoBehaviour
{
    [SerializeField] string sceneName;

    public void Setup(string na)
    {
        sceneName = na;
    }

    public void SceneChange()
    {
        SceneManager.LoadScene(sceneName);
    }
}
