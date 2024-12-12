using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyGroupManagerScene : MonoBehaviour
{
    [System.Serializable]
    public class EnemyGroup
    {
        public GameObject[] enemies;
        public GameObject[] spawnEffects;
    }

    [Header("Enemy Waves")]
    public List<EnemyGroup> enemyWaves;
    private int currentWaveIndex = 0;

    [Header("Next Scene Name")]
    public string nextSceneName;

    void Start()
    {
        ActivateWave(currentWaveIndex);
    }

    void Update()
    {
        if (currentWaveIndex < enemyWaves.Count)
        {
            if (AllEnemiesDead(enemyWaves[currentWaveIndex]))
            {
                currentWaveIndex++;
                if (currentWaveIndex < enemyWaves.Count)
                {
                    ActivateWave(currentWaveIndex);
                }
                else
                {
                    Debug.Log("All waves completed! Switching to next scene...");
                    StartCoroutine(SwitchSceneAfterDelay(2f));
                }
            }
        }
    }

    bool AllEnemiesDead(EnemyGroup group)
    {
        foreach (var enemy in group.enemies)
        {
            if (enemy != null)
            {
                return false;
            }
        }
        return true;
    }

    void ActivateWave(int waveIndex)
    {
        ActivateWaveImmediately(waveIndex);
    }

    void ActivateWaveImmediately(int waveIndex)
    {
        EnemyGroup group = enemyWaves[waveIndex];

        foreach (var effect in group.spawnEffects)
        {
            if (effect != null)
            {
                effect.SetActive(true);
                StartCoroutine(DeactivateEffect(effect, 1f));
            }
        }

        foreach (var enemy in group.enemies)
        {
            if (enemy != null)
            {
                enemy.SetActive(true);
            }
        }

        Debug.Log($"Wave {waveIndex + 1} activated!");
    }

    IEnumerator DeactivateEffect(GameObject effect, float delay)
    {
        yield return new WaitForSeconds(delay);
        effect.SetActive(false);
    }

    public void DeactivateAllWaves()
    {
        foreach (var group in enemyWaves)
        {
            foreach (var enemy in group.enemies)
            {
                if (enemy != null)
                {
                    enemy.SetActive(false);
                }
            }

            foreach (var effect in group.spawnEffects)
            {
                if (effect != null)
                {
                    effect.SetActive(false);
                }
            }
        }
    }

    IEnumerator SwitchSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
