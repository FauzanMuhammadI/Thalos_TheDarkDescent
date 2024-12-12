using System.Collections;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
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
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * (360f / bulletCount);
            Quaternion rotation = Quaternion.Euler(0f, angle, 0f);

            GameObject spawnedBullet = Instantiate(bullet, transform.position, rotation);

            BulletController bulletController = spawnedBullet.GetComponent<BulletController>();
            if (bulletController)
            {
                bulletController.speed = bulletSpeed;
                bulletController.bulletLife = bulletLife;
            }
        }
    }
}
