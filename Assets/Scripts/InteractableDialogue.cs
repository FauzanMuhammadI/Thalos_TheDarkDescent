using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableDialogue : MonoBehaviour
{
    [SerializeField] private UnityEvent onInteract;
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private float interactionRadius = 2f; 
    [SerializeField] private LayerMask playerLayer;

    private bool allowInteract;

    private void Update()
    {
        DetectPlayer();

        if (allowInteract && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void DetectPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionRadius, playerLayer);
        allowInteract = hitColliders.Length > 0;

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
