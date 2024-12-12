using System.Collections;
using UnityEngine;

public class BulletFollowerSpawner : MonoBehaviour
{
    [Header("Bullet Attributes")]
    public GameObject bullet;
    public float bulletLife;
    public float bulletSpeed;

    [Header("Spawner Attributes")]
    [SerializeField] private int bulletCount;
    [SerializeField] private float firingRate;
    [SerializeField] private bool rotateSpawner = true;
    [SerializeField] private float rotationSpeed;

    [Header("Player Target")]
    public Transform player; // Player reference

    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;

        if (rotateSpawner)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }

        if (timer >= firingRate)
        {
            FireBullets();
            timer = 0f;
        }
    }

    private void FireBullets()
    {
        if (player == null)
        {
            Debug.LogWarning("Player reference is not assigned!");
            return;
        }

        for (int i = 0; i < bulletCount; i++)
        {
            // Calculate the direction to the player (ignore Y component for rotation)
            Vector3 directionToPlayer = (player.position - transform.position).normalized;

            // Set the direction to face the player, keeping the Y component the same as the spawner
            directionToPlayer.y = 0; // Remove Y direction component

            // Create the rotation based on the horizontal direction
            Quaternion rotation = Quaternion.LookRotation(directionToPlayer);

            // Instantiate bullet
            GameObject spawnedBullet = Instantiate(bullet, transform.position, rotation);

            // Set bullet properties
            BulletController bulletController = spawnedBullet.GetComponent<BulletController>();
            if (bulletController)
            {
                bulletController.speed = bulletSpeed;
                bulletController.bulletLife = bulletLife;
            }
        }
    }
}
