using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    private static Vector3 respawnPosition;
    private static bool checkpointActivated = false;
    public AudioManager audioManager;

    [Header("Fireplace Settings")]
    public GameObject fireplaceOff;
    public GameObject fireplaceOn;

    private bool isActivated = false;

    private void Start()
    {
        if (!checkpointActivated)
        {
            respawnPosition = GameObject.FindWithTag("Player").transform.position;
        }

        if (fireplaceOff != null) fireplaceOff.SetActive(true);
        if (fireplaceOn != null) fireplaceOn.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isActivated)
        {
            ActivateCheckpoint();
        }
    }

    private void ActivateCheckpoint()
    {
        isActivated = true;
        checkpointActivated = true;
        respawnPosition = transform.position;
        Debug.Log("Checkpoint activated at position: " + respawnPosition);

        if (fireplaceOff != null) fireplaceOff.SetActive(false);
        if (fireplaceOn != null) fireplaceOn.SetActive(true);

        PlayCheckpointAudio();
    }

    public void PlayCheckpointAudio()
    {
        if (audioManager != null && audioManager.SFXSource != null && audioManager.checkpoint != null)
        {
            audioManager.SFXSource.clip = audioManager.checkpoint;
            audioManager.SFXSource.Play();
        }
    }

    public static Vector3 GetRespawnPosition()
    {
        return respawnPosition;
    }

    public static bool HasCheckpoint()
    {
        return checkpointActivated;
    }

    public static void ResetCheckpoint()
    {
        checkpointActivated = false;
        respawnPosition = Vector3.zero;
        Debug.Log("Checkpoint reset.");
    }
}
