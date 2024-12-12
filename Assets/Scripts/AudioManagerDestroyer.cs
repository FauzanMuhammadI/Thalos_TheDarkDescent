using UnityEngine;

public class AudioManagerDestroyer : MonoBehaviour
{
    private void Start()
    {
        // Search for an object named "AudioManager" in the hierarchy
        GameObject audioManager = GameObject.Find("AudioManager");

        // If the object exists, destroy it
        if (audioManager != null)
        {
            Destroy(audioManager);
            Debug.Log("AudioManager has been destroyed.");
        }
        else
        {
            Debug.LogWarning("AudioManager not found in the hierarchy.");
        }
    }
}
