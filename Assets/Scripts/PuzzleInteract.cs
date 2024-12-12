using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleInteract : MonoBehaviour
{
    [SerializeField] private Canvas canvasToToggle;
    [SerializeField] private Camera cameraToToggle;
    [Header("Gate")]
    public GameObject gate;
    private bool isPlayerInside = false;
    private bool isCameraDisabled = false; 

    private void Start()
    {
        if (canvasToToggle != null)
        {
            canvasToToggle.gameObject.SetActive(false);
        }
        if (cameraToToggle != null)
        {
            cameraToToggle.gameObject.SetActive(true); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            if (canvasToToggle != null)
            {
                canvasToToggle.gameObject.SetActive(true); 
            }
            DeactivateGate();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            if (canvasToToggle != null)
            {
                canvasToToggle.gameObject.SetActive(false); 
            }
        }
    }

    private void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.E))
        {
            if (cameraToToggle != null)
            {
                isCameraDisabled = !isCameraDisabled; 
                cameraToToggle.gameObject.SetActive(!isCameraDisabled); 
            }
        }
    }

    void DeactivateGate()
    {
        if (gate != null && gate.activeSelf)
        {
            gate.SetActive(false);
        }
    }
}
