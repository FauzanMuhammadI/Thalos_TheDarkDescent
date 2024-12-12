using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    [SerializeField] private int damage = 10; // Jumlah damage yang diberikan peluru

    private void OnTriggerEnter(Collider other)
    {
        // Periksa apakah objek yang terkena adalah pemain
        if (other.CompareTag("Player"))
        {
            // Ambil komponen Health dari pemain
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                // Kurangi kesehatan pemain
                playerHealth.TakeDamage(damage);
            }

            // Hancurkan peluru setelah mengenai pemain
            Destroy(gameObject);
        }
    }
}
