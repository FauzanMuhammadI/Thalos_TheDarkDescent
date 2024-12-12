using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneTartarus : MonoBehaviour
{
    public BossHealth bossHealth;
    public string nextSceneName;

    private void Start()
    {
        if (bossHealth == null)
        {
            Debug.LogError("BossHealth reference is missing in BossDefeatHandler.");
        }

        if (string.IsNullOrEmpty(nextSceneName))
        {
            Debug.LogError("Next scene name is not set in BossDefeatHandler.");
        }
    }

    private void Update()
    {
        if (bossHealth != null && bossHealth.currentHealth <= 0)
        {
            StartCoroutine(TransitionAfterBossDefeat());
        }
    }

    IEnumerator TransitionAfterBossDefeat()
    {
        yield return new WaitForSeconds(2f);

        if (!string.IsNullOrEmpty(nextSceneName))
        {
            CheckpointController.ResetCheckpoint();

            SceneManager.LoadScene(nextSceneName);
        }
    }
}
