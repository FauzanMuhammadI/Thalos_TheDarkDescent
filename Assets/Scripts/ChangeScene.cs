using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private string targetSceneName;
    [SerializeField] private string targetSceneName2;

    void Update()
    {
        // Periksa jika tombol "N" ditekan
        if (Input.GetKeyDown(KeyCode.M))
        {
            SwitchScene();
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            SwitchScene2();
        }
    }

    private void SwitchScene()
    {
        // Pastikan nama scene tidak kosong dan scene tersebut ada di build settings
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            Debug.LogError("Target scene name is not set or is invalid.");
        }
    }private void SwitchScene2()
    {
        // Pastikan nama scene tidak kosong dan scene tersebut ada di build settings
        if (!string.IsNullOrEmpty(targetSceneName2))
        {
            SceneManager.LoadScene(targetSceneName2);
        }
        else
        {
            Debug.LogError("Target scene name is not set or is invalid.");
        }
    }
}

