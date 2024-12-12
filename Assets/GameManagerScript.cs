using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject gameOverUI;
    public Health playerHealth;

    void Start()
    {
        gameOverUI.SetActive(false);
    }

    public void gameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void restart()
    {
        Time.timeScale = 1f;
        if (playerHealth != null && CheckpointController.HasCheckpoint())
        {
            playerHealth.RespawnPlayer();
            gameOverUI.SetActive(false);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            GameObject audioManager = GameObject.Find("AudioManager");
            if (audioManager != null)
            {
                Destroy(audioManager);
            }
        }
    }
}
