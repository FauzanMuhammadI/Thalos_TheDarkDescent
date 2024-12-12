using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupErebosManager : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public List<GameObject> enemies;  // List of enemies to activate
        public List<GameObject> spawnEffects;  // List of effects to activate
        public int healthThreshold;  // Boss health threshold to activate this wave
    }

    public List<Wave> waves = new List<Wave>();  // List of all waves
    private int currentWaveIndex = 0;  // Index to track the current wave
    public ErebosHealth erebosHealth;  // Reference to the ErebosHealth script

    void Start()
    {
        erebosHealth.onHealthChanged += CheckForWave;  // Subscribe to health changes
    }

    // Called whenever the boss health changes
    private void CheckForWave(int currentHealth)
    {
        if (currentWaveIndex < waves.Count && currentHealth <= waves[currentWaveIndex].healthThreshold)
        {
            StartWave(waves[currentWaveIndex]);
            currentWaveIndex++;
        }
    }

    // Activates the enemies and effects for the current wave
    private void StartWave(Wave wave)
    {
        // Activate enemies
        foreach (GameObject enemy in wave.enemies)
        {
            enemy.SetActive(true);  // Enable the enemy game object
        }

        // Activate spawn effects
        foreach (GameObject effect in wave.spawnEffects)
        {
            effect.SetActive(true);
            StartCoroutine(DeactivateEffect(effect, 1f));
        }

        Debug.Log("Wave started: " + wave.healthThreshold);
    }

    IEnumerator DeactivateEffect(GameObject effect, float delay)
    {
        yield return new WaitForSeconds(delay);
        effect.SetActive(false);
    }

    // You can add a function to reset the waves if needed
    public void ResetWaves()
    {
        currentWaveIndex = 0;
    }
}
