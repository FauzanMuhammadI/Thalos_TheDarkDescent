using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AutomaticDialogue : MonoBehaviour
{
    [SerializeField] private UnityEvent onInteract;
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private float interactionRadius = 2f;
    [SerializeField] private LayerMask playerLayer;
    
    //public GameObject dialogSystem;

    private bool hasTriggeredDialogue = false;

    private void Update()
    {
        DetectPlayer();
    }

    private void DetectPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionRadius, playerLayer);
        if (hitColliders.Length > 0 && !hasTriggeredDialogue)
        {
           // dialogSystem.SetActive(true);
            Interact();

            hasTriggeredDialogue = true;
        }
    }

    private void Interact()
    {
        onInteract.Invoke(); 
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
