using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossReset : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private bool initialActiveState;

    private BossHealth bossHealth;

    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialActiveState = gameObject.activeSelf;

        bossHealth = GetComponent<BossHealth>();
    }

    public void ResetBoss()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        gameObject.SetActive(initialActiveState);

        if (bossHealth != null)
        {
            bossHealth.ResetHealth();
        }

        Debug.Log("Boss has been reset!");
    }
}
