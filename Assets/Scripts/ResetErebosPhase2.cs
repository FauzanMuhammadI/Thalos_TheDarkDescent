using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetErebosPhase2 : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private bool initialActiveState;

    private ErebosHealth erebosHealth;

    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialActiveState = gameObject.activeSelf;

        erebosHealth = GetComponent<ErebosHealth>();

    }

    public void ResetBoss()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        gameObject.SetActive(initialActiveState);

        if (erebosHealth != null)
        {
            erebosHealth.ResetHealth();
        }

        gameObject.SetActive(false);

        Debug.Log("Boss has been reset and deactivated!");
    }
}
