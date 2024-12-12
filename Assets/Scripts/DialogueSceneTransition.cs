using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueSceneTransition : MonoBehaviour
{
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private string nextSceneName;

    private void Update()
    {
        if (dialogueUI != null && !dialogueUI.IsDialogActive)
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        GameObject audioManager = GameObject.Find("AudioManager");
        if (audioManager != null)
        {
            Destroy(audioManager);
        }

        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
