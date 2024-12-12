using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErebosActivation : MonoBehaviour
{
    public GameObject boss;
    public GameObject erebos;
    public LayerMask Player;

    private void Start()
    {
        if (boss != null)
        {
            boss.SetActive(false);
        }

        if (erebos != null)
        {
            erebos.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((Player.value & (1 << other.gameObject.layer)) > 0)
        {
            if (boss != null && !boss.activeSelf)
            {
                boss.SetActive(true);
                Debug.Log("Boss Activated!");
            }

            if (erebos != null && !erebos.activeSelf)
            {
                erebos.SetActive(true);
                Debug.Log("Erebos Activated!");
            }
        }
    }
}

