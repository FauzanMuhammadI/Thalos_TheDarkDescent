using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivation : MonoBehaviour
{
    public GameObject boss; 
    public LayerMask Player; 

    private void Start()
    {
        if (boss != null)
        {
            boss.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((Player.value & (1 << other.gameObject.layer)) > 0)
        {
            if (boss != null)
            {
                boss.SetActive(true);
                Debug.Log("Boss Activated!");
            }
        }
    }
}

