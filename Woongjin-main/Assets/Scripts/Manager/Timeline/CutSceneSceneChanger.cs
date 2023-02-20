using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneSceneChanger : MonoBehaviour
{
    [SerializeField] string stst;

    private void Awake()
    {
        ChangeScene();
    }

    private void OnEnable()
    {
        ChangeScene();
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(stst);
    }
}
