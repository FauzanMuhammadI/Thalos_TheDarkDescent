using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletErebosSpawner : MonoBehaviour
{
    [Header("Bullet Attributes")]
    public GameObject bullet;
    public float bulletLife;
    public float bulletSpeed;

    [Header("Spawner Attributes")]
    [SerializeField] private int bulletCountRate1;
    [SerializeField] private float firingRate1 = 5f; // Duration for attack rate1
    [SerializeField] private int bulletCountRate2;
    [SerializeField] private float firingRate2 = 0.5f; // Shorter duration for attack rate2
    [SerializeField] private bool rotateSpawner = true;
    [SerializeField] private float rotationSpeed;

    private float timer = 0f;
    private bool isAttackRate2Active = false;

    private void Update()
    {
        timer += Time.deltaTime;

        if (rotateSpawner)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }

        if (!isAttackRate2Active && timer >= firingRate1)
        {
            // Transition to attack rate2
            StartCoroutine(ActivateAttackRate2());
            timer = 0f;
        }

        if (!isAttackRate2Active && timer >= firingRate1)
        {
            FireBullets(bulletCountRate1); // Fire bullets for attack rate1
            timer = 0f;
        }
    }

    private void FireBullets(int bulletCount)
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

    private IEnumerator ActivateAttackRate2()
    {
        isAttackRate2Active = true;
        float attackRate2Timer = 0f;

        while (attackRate2Timer < firingRate1) // Active for the duration of attack rate1
        {
            FireBullets(bulletCountRate2); // Fire bullets for attack rate2
            attackRate2Timer += firingRate2;
            yield return new WaitForSeconds(firingRate2);
        }

        isAttackRate2Active = false; // Deactivate attack rate2
        timer = 0f; // Reset timer for attack rate1
    }
}

