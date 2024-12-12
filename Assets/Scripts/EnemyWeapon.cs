using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private float weaponHitRadius;
    [SerializeField] private int damage = 2;
    [SerializeField] private LayerMask targetLayer;

    private float attackCooldown = 1f;
    private float lastAttackTime;

    void Update()
    {
        DetectHit();
    }

    private void DetectHit()
    {
        if (Time.time < lastAttackTime + attackCooldown) return;

        Collider[] hit = Physics.OverlapSphere(transform.position, weaponHitRadius, targetLayer);

        if (hit.Length > 0)
        {
            Health targetHealth = hit[0].GetComponent<Health>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(damage); 
            }
            lastAttackTime = Time.time;
        }
    }

    private void OnDrawGizmos()
    {


        // Gambar jarak pengejaran
        Gizmos.color = Color.green; // Ubah warna gizmo menjadi merah
        Gizmos.DrawWireSphere(transform.position, weaponHitRadius); // Gambar lingkaran wireframe di sekitar musuh


    }
}
