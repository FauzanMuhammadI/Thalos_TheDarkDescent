using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AutoActiveDialogue : MonoBehaviour
{
    [SerializeField] private UnityEvent onDialogueStart;
    [SerializeField] private float dialogueDelay = 2f; // Waktu tunda sebelum dialog dimulai

    private bool hasStartedDialogue = false;

    private void Start()
    {
        StartCoroutine(StartDialogueAfterDelay());
    }

    private IEnumerator StartDialogueAfterDelay()
    {
        yield return new WaitForSeconds(dialogueDelay);

        if (!hasStartedDialogue)
        {
            StartDialogue();
            hasStartedDialogue = true;
        }
    }

    private void StartDialogue()
    {
        onDialogueStart.Invoke();
    }
}
