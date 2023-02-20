using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleSceneLoader : MonoBehaviour
{
    [SerializeField] string sceneName;

    public void GotoScene()
    {
        SceneManager.LoadScene(sceneName);
    }
    
}
