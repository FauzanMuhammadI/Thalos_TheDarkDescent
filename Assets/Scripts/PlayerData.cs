using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    public int health;
    public float[] position;
    public int sceneIndex;

    public PlayerData(PlayerController player, Health healthComponent)
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        health = healthComponent.currentHealth;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
