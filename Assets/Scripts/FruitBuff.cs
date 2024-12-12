//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class FruitBuff : MonoBehaviour
//{
//    [Header("Enemy Manager")]
//    public EnemyGroupManager enemyGroupManager;

//    [Header("Fruit")]
//    public GameObject fruit;

//    private bool isFruitActive = false; 

//    private void Start()
//    {
//        if (fruit != null)
//        {
//            fruit.SetActive(false);
//        }
//    }

//    private void Update()
//    {
//        if (!isFruitActive && enemyGroupManager != null && AllWavesCleared())
//        {
//            FruitActive();
//        }
//    }

//    bool AllWavesCleared()
//    {
//        if (enemyGroupManager == null) return false;

//        foreach (var group in enemyGroupManager.enemyWaves)
//        {
//            foreach (var enemy in group.enemies)
//            {
//                if (enemy != null)
//                {
//                    return false; 
//                }
//            }
//        }
//        return true; 
//    }

//    void FruitActive()
//    {
//        if (fruit != null)
//        {
//            fruit.SetActive(true);
//            isFruitActive = true;
//        }
//    }
//}
