using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public LayerMask bossLayers;
    public LayerMask erebosLayers;
    public int attackDamage = 40;
    public float attackRate = 2f;
    public float attackDashDistance = 2f; 

    public bool isAttacking = false;
    public static PlayerCombat instance;

    private PlayerController playerController;
    public AudioManager audioManager;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Time.time >= attackRate)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
                attackRate = Time.time;
            }
        }

        FlipAttackPoint();
    }

    void FlipAttackPoint()
    {
        float horizontal = PlayerCombat.instance.animator.GetFloat("Horizontal");

        if (horizontal == -1f)
        {
            attackPoint.localPosition = new Vector3(-9.5f, attackPoint.localPosition.y, attackPoint.localPosition.z);
        }
        else if (horizontal == 1f)
        {
            attackPoint.localPosition = new Vector3(9.5f, attackPoint.localPosition.y, attackPoint.localPosition.z);
        }
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking)
        {
            isAttacking = true;
            Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider enemy in hitEnemies)
            {
                enemy.GetComponent<EnemiesDamaged>().TakeDamage(attackDamage);
            }

            Collider[] hitBosses = Physics.OverlapSphere(attackPoint.position, attackRange, bossLayers);
            foreach (Collider boss in hitBosses)
            {
                boss.GetComponent<BossHealth>().TakeDamage(attackDamage);
            }

            Collider[] hitEreboses = Physics.OverlapSphere(attackPoint.position, attackRange, erebosLayers);
            foreach (Collider erebos in hitEreboses)
            {
                erebos.GetComponent<ErebosHealth>().TakeDamage(attackDamage);
            }
        }
    }

    public void PlayAttackAudio1()
    {
        if (audioManager != null && audioManager.SFXSource != null)
        {
            audioManager.SFXSource.clip = audioManager.attack1;
            audioManager.SFXSource.Play();
        }
    }

    public void PlayAttackAudio2()
    {
        if (audioManager != null && audioManager.SFXSource != null)
        {
            audioManager.SFXSource.clip = audioManager.attack2;
            audioManager.SFXSource.Play();
        }
    }

    public void PlayAttackAudio3()
    {
        if (audioManager != null && audioManager.SFXSource != null)
        {
            audioManager.SFXSource.clip = audioManager.attack3;
            audioManager.SFXSource.Play();
        }
    }


    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
