using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenemanagement : MonoBehaviour
{
    
    public string sceneName; // The name of the scene to switch to

    public void SwitchScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}

