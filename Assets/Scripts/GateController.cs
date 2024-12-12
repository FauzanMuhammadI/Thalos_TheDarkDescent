using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    [Header("Enemy Manager")]
    public EnemyGroupManager enemyGroupManager; 

    [Header("Gate")]
    public GameObject gate;

    [Header("Fruit")]
    public GameObject fruit;

    private void Start()
    {
        if (fruit != null)
        {
            fruit.SetActive(false);
        }
    }

    void Update()
    {
        if (enemyGroupManager != null && AllWavesCleared())
        {
            DeactivateGate();
            FruitActive();
        }
    }

    bool AllWavesCleared()
    {
        foreach (var group in enemyGroupManager.enemyWaves)
        {
            foreach (var enemy in group.enemies)
            {
                if (enemy != null) 
                {
                    return false;
                }
            }
        }
        return true;
    }

    void DeactivateGate()
    {
        if (gate != null && gate.activeSelf)
        {
            gate.SetActive(false);
            Debug.Log("Gate is now deactivated!");
        }
    }

    void FruitActive()
    {
        if (fruit != null)
        {
            fruit.SetActive(true);
        }
    }
}
