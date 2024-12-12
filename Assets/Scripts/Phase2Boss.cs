using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2Boss : MonoBehaviour
{
    [SerializeField] private DialogueUI dialogueUI; 
    [SerializeField] private GameObject boss2; 

    private bool boss2Activated = false;

    private void Start()
    {
        if (dialogueUI == null)
        {
            Debug.LogError("DialogueUI tidak diatur di BossDialogHandler.");
        }

        if (boss2 != null)
        {
            boss2.SetActive(false);
        }
        else
        {
            Debug.LogError("Boss2 tidak diatur di BossDialogHandler.");
        }
    }

    private void Update()
    {
        if (!dialogueUI.IsDialogActive && !boss2Activated)
        {
            ActivateBoss2();
        }
    }

    private void ActivateBoss2()
    {
        if (boss2 != null)
        {
            boss2.SetActive(true); // Aktifkan boss2
            Debug.Log("Boss2 diaktifkan.");
            boss2Activated = true; // Pastikan hanya aktif sekali
        }
        else
        {
            Debug.LogError("GameObject Boss2 tidak ditemukan.");
        }
    }
}