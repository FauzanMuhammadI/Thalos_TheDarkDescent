using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField] private GameObject boss; 
    [SerializeField] private GameObject bossDialog;
    [SerializeField] private GameObject dialogSystem;

    private Animator bossAnimator;
    private bool isBossDead = false;

    private void Start()
    {
        if (boss != null)
        {
            bossAnimator = boss.GetComponent<Animator>();
        }
        else
        {
            Debug.LogWarning("Boss GameObject tidak diatur.");
        }

        if (bossDialog != null)
        {
            bossDialog.SetActive(false); 
        }
        else
        {
            Debug.LogWarning("Boss Dialog GameObject tidak diatur.");
        }

        if (dialogSystem != null)
        {
            dialogSystem.SetActive(false);
        }
    }

    private void Update()
    {
        if (boss == null && !isBossDead)
        {
            dialogSystem.SetActive(true);
            ActivateBossDialog();
            isBossDead = true;
        }
        else if (bossAnimator != null && bossAnimator.GetCurrentAnimatorStateInfo(0).IsName("Death") && !isBossDead)
        {
            if (bossAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                ActivateBossDialog();
                isBossDead = true;
            }
        }
    }

    private void ActivateBossDialog()
    {
        if (bossDialog != null)
        {
            bossDialog.SetActive(true); 
            Debug.Log("Dialog bos diaktifkan.");
        }
        else
        {
            Debug.LogError("Dialog bos tidak ditemukan.");
        }
    }
}

